namespace Workflow.Engine.Models
{
    /// <summary>
    /// 活动连接定义。
    /// </summary>
    public class TransitionDefinition
    {
        /// <summary>
        /// 源活动。
        /// </summary>
        public ActivityDefinition SourceActivity { get; set; }

        /// <summary>
        /// 目标活动。
        /// </summary>
        public ActivityDefinition DestinationActivity { get; set; }

        /// <summary>
        /// 工作流定义。
        /// </summary>
        public WorkflowDefinition WorkflowDefinition { get; set; }
    }
}