using System.Collections.Generic;

namespace Workflow.Engine
{
    /// <summary>
    /// 表示一个任务。
    /// </summary>
    public abstract class Task : IActivity
    {
        #region Implementation of IActivity

        /// <summary>
        /// 名称。
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 分类。
        /// </summary>
        public abstract string Category { get; }

        /// <summary>
        /// 描述。
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// 是否可以执行。
        /// </summary>
        /// <param name="workflowContext">工作流上下文。</param>
        /// <param name="activityContext">活动上下文。</param>
        /// <returns></returns>
        public virtual bool CanExecute(WorkflowContext workflowContext, ActivityContext activityContext)
        {
            return true;
        }

        /// <summary>
        /// 执行活动。
        /// </summary>
        /// <param name="workflowContext">工作流上下文。</param>
        /// <param name="activityContext">活动上下文。</param>
        /// <returns>接下来需要执行的动作。</returns>
        public abstract IEnumerable<string> Execute(WorkflowContext workflowContext, ActivityContext activityContext);

        /// <summary>
        /// 工作流开始前执行。
        /// </summary>
        /// <param name="workflowContext">工作流上下文。</param>
        /// <param name="cancellationToken">可用于取消的令牌。</param>
        public virtual void OnWorkflowStarting(WorkflowContext workflowContext, CancellationToken cancellationToken)
        {
        }

        /// <summary>
        /// 工作流开始之后执行。
        /// </summary>
        /// <param name="workflowContext">工作流上下文。</param>
        public virtual void OnWorkflowStarted(WorkflowContext workflowContext)
        {
        }

        /// <summary>
        /// 工作流恢复前执行。
        /// </summary>
        /// <param name="workflowContext">工作流上下文。</param>
        /// <param name="cancellationToken">可用于取消的令牌。</param>
        public virtual void OnWorkflowResuming(WorkflowContext workflowContext, CancellationToken cancellationToken)
        {
        }

        /// <summary>
        /// 工作流恢复之后执行。
        /// </summary>
        /// <param name="workflowContext">工作流上下文。</param>
        public virtual void OnWorkflowResumed(WorkflowContext workflowContext)
        {
        }

        /// <summary>
        /// 活动执行前执行。
        /// </summary>
        /// <param name="workflowContext">工作流上下文。</param>
        /// <param name="activityContext">活动上下文。</param>
        /// <param name="cancellationToken">可用于取消的令牌。</param>
        public virtual void OnActivityExecuting(WorkflowContext workflowContext, ActivityContext activityContext,
            CancellationToken cancellationToken)
        {
        }

        /// <summary>
        /// 活动执行后执行。
        /// </summary>
        /// <param name="workflowContext">工作流上下文。</param>
        /// <param name="activityContext">活动上下文。</param>
        public virtual void OnActivityExecuted(WorkflowContext workflowContext, ActivityContext activityContext)
        {
        }

        #endregion Implementation of IActivity
    }
}