using System;
using System.Collections.Generic;
using Workflow.Engine;
using Workflow.Engine.Services;
using Workflow.Provider.Memory;

namespace Shell
{
    internal class Task1 : Task
    {
        #region Implementation of IActivity

        /// <summary>
        /// 名称。
        /// </summary>
        public override string Name => "Task1";

        /// <summary>
        /// 分类。
        /// </summary>
        public override string Category => "Default";

        /// <summary>
        /// 描述。
        /// </summary>
        public override string Description { get; }

        /// <summary>
        /// 执行活动。
        /// </summary>
        /// <param name="workflowContext">工作流上下文。</param>
        /// <param name="activityContext">活动上下文。</param>
        /// <returns>接下来需要执行的动作。</returns>
        public override IEnumerable<string> Execute(WorkflowContext workflowContext, ActivityContext activityContext)
        {
            Console.WriteLine(GetType().FullName);
            return new[] { "Task2" };
        }

        #endregion Implementation of IActivity
    }

    internal class Task2 : Task
    {
        #region Implementation of IActivity

        /// <summary>
        /// 名称。
        /// </summary>
        public override string Name => "Task2";

        /// <summary>
        /// 分类。
        /// </summary>
        public override string Category => "Default";

        /// <summary>
        /// 描述。
        /// </summary>
        public override string Description { get; }

        /// <summary>
        /// 执行活动。
        /// </summary>
        /// <param name="workflowContext">工作流上下文。</param>
        /// <param name="activityContext">活动上下文。</param>
        /// <returns>接下来需要执行的动作。</returns>
        public override IEnumerable<string> Execute(WorkflowContext workflowContext, ActivityContext activityContext)
        {
            Console.WriteLine(GetType().FullName);
            return new string[0];
        }

        #endregion Implementation of IActivity
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            IActivitiesManager activitiesManager = new ActivitiesManager(new IActivity[] { new Task1(), new Task2() });
            IActivityDefinitionService activityDefinitionService = new ActivityDefinitionService();
            IWorkflowManager workflowManager = new WorkflowManager(activitiesManager, activityDefinitionService);
            workflowManager.TriggerEvent("Task1", () => new Dictionary<string, object>());
        }
    }
}