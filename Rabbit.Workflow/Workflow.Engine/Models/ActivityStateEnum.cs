namespace Workflow.Engine.Models
{
    /// <summary>
    /// 活动状态枚举。
    /// </summary>
    public enum ActivityStateEnum
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
        /// 处理完成。
        /// </summary>
        Processed = 3
    }
}