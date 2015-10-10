namespace Workflow.Engine.Models
{
    /// <summary>
    /// 等待执行的活动定义。
    /// </summary>
    public class AwaitingActivityDefinition
    {
        /// <summary>
        /// 活动定义。
        /// </summary>
        public ActivityDefinition ActivityDefinition { get; set; }

        /// <summary>
        /// 工作流实例。
        /// </summary>
        public WorkflowInstance WorkflowInstance { get; set; }

        /// <summary>
        /// 活动状态。
        /// </summary>
        public ActivityStateEnum State { get; set; }

        /// <summary>
        /// 设置状态为完成。
        /// </summary>
        public void Complete()
        {
            State = ActivityStateEnum.Processed;
        }
    }
}