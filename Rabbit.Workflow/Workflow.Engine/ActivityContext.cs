using System.Collections.Generic;

namespace Workflow.Engine
{
    /// <summary>
    /// 活动上下文。
    /// </summary>
    public class ActivityContext
    {
        /// <summary>
        /// 活动实例。
        /// </summary>
        public IActivity Activity { get; set; }

        /// <summary>
        /// 上下文环境。
        /// </summary>
        public IDictionary<string, object> Environment { get; set; }
    }
}