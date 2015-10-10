using System.Collections.Generic;
using System.Linq;
using Workflow.Engine.Models;
using Workflow.Engine.Services;

namespace Workflow.Provider.Memory
{
    public sealed class ActivityDefinitionService : IActivityDefinitionService
    {
        private static readonly List<WorkflowDefinition> WorkflowDefinitions = new List<WorkflowDefinition>();

        static ActivityDefinitionService()
        {
            var workflowDefinition = new WorkflowDefinition
            {
                Name = "流程1"
            };
            var definition2 = new ActivityDefinition
            {
                Name = "Task2",
                Start = false,
                //                State = null,
                WorkflowDefinition = workflowDefinition
            };
            var definition = new ActivityDefinition
            {
                Name = "Task1",
                Start = true,
                //                State = null,
                WorkflowDefinition = workflowDefinition
            };
            workflowDefinition.Activitys = new[] { definition, definition2 };
            workflowDefinition.Transitions = new List<TransitionDefinition>
            {
                new TransitionDefinition
                {
                    SourceActivity = definition,
                    DestinationActivity = definition2,
                    WorkflowDefinition = workflowDefinition
                }
            };

            WorkflowDefinitions.Add(workflowDefinition);
        }

        #region Implementation of IActivityDefinitionService

        public IEnumerable<ActivityDefinition> GetStartActivityDefinitions(string name)
        {
            return WorkflowDefinitions.SelectMany(i => i.Activitys.Where(z => z.Start && z.Name == name)).ToArray();
        }

        public IEnumerable<AwaitingActivityDefinition> GetAwaitingActivityDefinitions(string name)
        {
            /*return new[]
            {
                new AwaitingActivityDefinition
                {
                    ActivityDefinition = new ActivityDefinition(),
                    WorkflowInstance = new WorkflowInstance()
                }
            }*/
            return new AwaitingActivityDefinition[0];
        }

        #endregion Implementation of IActivityDefinitionService
    }
}