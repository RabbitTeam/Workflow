using System.Collections.Generic;

namespace Workflow.Engine.Models
{
    /// <summary>
    /// 工作流定义。
    /// </summary>
    public class WorkflowDefinition
    {
        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 包含的活动。
        /// </summary>
        public IList<ActivityDefinition> Activitys { get; set; }

        /// <summary>
        /// 活动连接定义。
        /// </summary>
        public IList<TransitionDefinition> Transitions { get; set; }
    }
}