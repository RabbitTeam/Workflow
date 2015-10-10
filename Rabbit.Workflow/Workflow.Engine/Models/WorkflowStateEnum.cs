namespace Workflow.Engine.Models
{
    /// <summary>
    /// 工作流状态枚举。
    /// </summary>
    public enum WorkflowStateEnum
    {
        /// <summary>
        /// 未处理。
        /// </summary>
        Untreated = 0,

        /// <summary>
        /// 处理中。
        /// </summary>
        Processing = 1,

        /// <summary>
        /// 挂起。
        /// </summary>
        Pending = 2,

        /// <summary>
        /// 处理完成。
        /// </summary>
        Processed = 3
    }
}