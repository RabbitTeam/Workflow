using System;
using System.Collections.Generic;
using System.Linq;
using Workflow.Engine.Models;

namespace Workflow.Engine.Services
{
    public interface IWorkflowManager
    {
        void TriggerEvent(string name, Func<Dictionary<string, object>> environmentContext);
    }

    public sealed class WorkflowManager : IWorkflowManager
    {
        private readonly IActivitiesManager _activitiesManager;
        private readonly IActivityDefinitionService _activityDefinitionService;

        public WorkflowManager(IActivitiesManager activitiesManager, IActivityDefinitionService activityDefinitionService)
        {
            _activitiesManager = activitiesManager;
            _activityDefinitionService = activityDefinitionService;
        }

        #region Implementation of IWorkflowManager

        public void TriggerEvent(string name, Func<Dictionary<string, object>> environmentContext)
        {
            var environment = environmentContext();
            var activity = _activitiesManager.GetActivityByName(name);

            var startedWorkflows = _activityDefinitionService.GetStartActivityDefinitions(name).AsQueryable();
            var awaitingActivities = _activityDefinitionService.GetAwaitingActivityDefinitions(name).AsQueryable();

            if (!startedWorkflows.Any() && !awaitingActivities.Any())
                return;

            foreach (var awaitingActivityDefinition in awaitingActivities)
            {
                var workflowContext = new WorkflowContext
                {
                    Environment = environment
                };

                var activityContext = CreateActivityContext(awaitingActivityDefinition.ActivityDefinition, environment);
                try
                {
                    if (!activity.CanExecute(workflowContext, activityContext))
                    {
                        continue;
                    }
                }
                catch (Exception e)
                {
                    continue;
                }

                ResumeWorkflow(awaitingActivityDefinition, workflowContext, environment);
            }

            foreach (var activityRecord in startedWorkflows)
            {
                var workflowContext = new WorkflowContext
                {
                    Environment = environment
                };

                var workflowRecord = new WorkflowInstance
                {
                    WorkflowDefinition = activityRecord.WorkflowDefinition
                };

                workflowContext.WorkflowInstance = workflowRecord;

                var activityContext = CreateActivityContext(activityRecord, environment);

                try
                {
                    if (!activity.CanExecute(workflowContext, activityContext))
                    {
                        continue;
                    }
                }
                catch (Exception e)
                {
                    continue;
                }

                StartWorkflow(workflowContext, activityRecord, environment);
            }
        }

        private void ResumeWorkflow(AwaitingActivityDefinition awaitingActivityDefinition, WorkflowContext workflowContext, Dictionary<string, object> environment)
        {
            var cancellationToken = new CancellationToken();
            InvokeActivities(activity => activity.OnWorkflowResuming(workflowContext, cancellationToken));
            if (cancellationToken.IsCancelled)
                return;
            InvokeActivities(activity => activity.OnWorkflowResumed(workflowContext));
            var workflowInstance = awaitingActivityDefinition.WorkflowInstance;
            workflowContext.WorkflowInstance = workflowInstance;
            workflowInstance.AwaitingActivities.Remove(awaitingActivityDefinition);
            var blockedOn = ExecuteWorkflow(workflowContext, awaitingActivityDefinition.ActivityDefinition, environment).ToList();

            if (!blockedOn.Any() && !workflowInstance.AwaitingActivities.Any())
            {
                awaitingActivityDefinition.WorkflowInstance.Complete();
            }
            else
            {
                foreach (var blocking in blockedOn)
                {
                    workflowInstance.AwaitingActivities.Add(new AwaitingActivityDefinition
                    {
                        ActivityDefinition = blocking,
                        WorkflowInstance = workflowInstance
                    });
                }
            }
        }

        private ActivityContext CreateActivityContext(ActivityDefinition activityDefinition, IDictionary<string, object> environment)
        {
            return new ActivityContext
            {
                Activity = _activitiesManager.GetActivityByName(activityDefinition.Name),
                Environment = environment
            };
        }

        private void InvokeActivities(Action<IActivity> action)
        {
            foreach (var activity in _activitiesManager.GetActivities())
            {
                action(activity);
            }
        }

        public IEnumerable<ActivityDefinition> ExecuteWorkflow(WorkflowContext workflowContext,
            ActivityDefinition activityDefinition, IDictionary<string, object> environment)
        {
            var scheduled = new Stack<ActivityDefinition>();

            scheduled.Push(activityDefinition);

            while (scheduled.Any())
            {
                activityDefinition = scheduled.Pop();

                var activityContext = CreateActivityContext(activityDefinition, environment);

                var cancellationToken = new CancellationToken();
                InvokeActivities(activity => activity.OnActivityExecuting(workflowContext, activityContext, cancellationToken));

                if (cancellationToken.IsCancelled)
                    continue;

                var outcomes = activityContext.Activity.Execute(workflowContext, activityContext).ToList();

                InvokeActivities(activity => activity.OnActivityExecuted(workflowContext, activityContext));

                foreach (var outcome in outcomes)
                {
                    var transition = workflowContext.WorkflowInstance.WorkflowDefinition.Transitions.FirstOrDefault(x => x.SourceActivity == activityDefinition && x.DestinationActivity.Name == outcome);

                    if (transition != null)
                    {
                        scheduled.Push(transition.DestinationActivity);
                    }
                }
            }
            return new ActivityDefinition[0];
        }

        private void StartWorkflow(WorkflowContext workflowContext, ActivityDefinition activityDefinition, IDictionary<string, object> environment)
        {
            var cancellationToken = new CancellationToken();
            InvokeActivities(activity => activity.OnWorkflowStarting(workflowContext, cancellationToken));

            if (cancellationToken.IsCancelled)
                return;

            InvokeActivities(activity => activity.OnWorkflowStarted(workflowContext));

            var blockedOn = ExecuteWorkflow(workflowContext, activityDefinition, environment).ToList();

            if (blockedOn.Any())
            {
                foreach (var blocking in blockedOn)
                {
                    workflowContext.WorkflowInstance.AwaitingActivities.Add(new AwaitingActivityDefinition
                    {
                        ActivityDefinition = blocking,
                        WorkflowInstance = workflowContext.WorkflowInstance
                    });
                }
            }
        }

        #endregion Implementation of IWorkflowManager
    }
}