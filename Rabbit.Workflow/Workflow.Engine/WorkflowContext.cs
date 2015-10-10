using System.Collections.Generic;
using Workflow.Engine.Models;

namespace Workflow.Engine
{
    /// <summary>
    /// 工作流上下文。
    /// </summary>
    public class WorkflowContext
    {
        /// <summary>
        /// 工作流实例。
        /// </summary>
        public WorkflowInstance WorkflowInstance { get; set; }

        /// <summary>
        /// 工作流上下文环境。
        /// </summary>
        public IDictionary<string, object> Environment { get; set; }
    }
}