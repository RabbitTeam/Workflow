using System.Collections.Generic;

namespace Workflow.Engine.Models
{
    /// <summary>
    /// 活动定义。
    /// </summary>
    public class ActivityDefinition
    {
        /// <summary>
        /// 活动名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 活动环境。
        /// </summary>
        public IDictionary<string, object> Environment { get; set; }

        /// <summary>
        /// 是否流程中起始的活动。
        /// </summary>
        public bool Start { get; set; }

        /// <summary>
        /// 工作流定义。
        /// </summary>
        public WorkflowDefinition WorkflowDefinition { get; set; }
    }
}