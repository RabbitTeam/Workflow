namespace Workflow.Engine
{
    /// <summary>
    /// 用于取消操作的令牌。
    /// </summary>
    public class CancellationToken
    {
        /// <summary>
        /// 取消操作。
        /// </summary>
        public void Cancel()
        {
            IsCancelled = true;
        }

        /// <summary>
        /// 是否取消。
        /// </summary>
        public bool IsCancelled { get; private set; }
    }
}