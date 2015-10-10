using System.Collections.Generic;

namespace Workflow.Engine.Models
{
    /// <summary>
    /// 工作流实例。
    /// </summary>
    public class WorkflowInstance
    {
        /// <summary>
        /// 工作流环境。
        /// </summary>
        public IDictionary<string, object> Environment { get; set; }

        /// <summary>
        /// 等待执行的活动。
        /// </summary>
        public virtual IList<AwaitingActivityDefinition> AwaitingActivities { get; set; }

        /// <summary>
        /// 工作流定义。
        /// </summary>
        public virtual WorkflowDefinition WorkflowDefinition { get; set; }

        /// <summary>
        /// 工作流状态。
        /// </summary>
        public WorkflowStateEnum State { get; set; }

        /// <summary>
        /// 设置状态为完成。
        /// </summary>
        public void Complete()
        {
            State = WorkflowStateEnum.Processed;
        }
    }
}