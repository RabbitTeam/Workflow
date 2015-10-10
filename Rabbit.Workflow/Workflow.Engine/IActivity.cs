using System.Collections.Generic;

namespace Workflow.Engine
{
    /// <summary>
    /// 一个抽象的活动。
    /// </summary>
    public interface IActivity
    {
        /// <summary>
        /// 名称。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 分类。
        /// </summary>
        string Category { get; }

        /// <summary>
        /// 描述。
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 是否可以执行。
        /// </summary>
        /// <param name="workflowContext">工作流上下文。</param>
        /// <param name="activityContext">活动上下文。</param>
        /// <returns></returns>
        bool CanExecute(WorkflowContext workflowContext, ActivityContext activityContext);

        /// <summary>
        /// 执行活动。
        /// </summary>
        /// <param name="workflowContext">工作流上下文。</param>
        /// <param name="activityContext">活动上下文。</param>
        /// <returns>接下来需要执行的动作。</returns>
        IEnumerable<string> Execute(WorkflowContext workflowContext, ActivityContext activityContext);

        /// <summary>
        /// 工作流开始前执行。
        /// </summary>
        /// <param name="workflowContext">工作流上下文。</param>
        /// <param name="cancellationToken">可用于取消的令牌。</param>
        void OnWorkflowStarting(WorkflowContext workflowContext, CancellationToken cancellationToken);

        /// <summary>
        /// 工作流开始之后执行。
        /// </summary>
        /// <param name="workflowContext">工作流上下文。</param>
        void OnWorkflowStarted(WorkflowContext workflowContext);

        /// <summary>
        /// 工作流恢复前执行。
        /// </summary>
        /// <param name="workflowContext">工作流上下文。</param>
        /// <param name="cancellationToken">可用于取消的令牌。</param>
        void OnWorkflowResuming(WorkflowContext workflowContext, CancellationToken cancellationToken);

        /// <summary>
        /// 工作流恢复之后执行。
        /// </summary>
        /// <param name="workflowContext">工作流上下文。</param>
        void OnWorkflowResumed(WorkflowContext workflowContext);

        /// <summary>
        /// 活动执行前执行。
        /// </summary>
        /// <param name="workflowContext">工作流上下文。</param>
        /// <param name="activityContext">活动上下文。</param>
        /// <param name="cancellationToken">可用于取消的令牌。</param>
        void OnActivityExecuting(WorkflowContext workflowContext, ActivityContext activityContext, CancellationToken cancellationToken);

        /// <summary>
        /// 活动执行后执行。
        /// </summary>
        /// <param name="workflowContext">工作流上下文。</param>
        /// <param name="activityContext">活动上下文。</param>
        void OnActivityExecuted(WorkflowContext workflowContext, ActivityContext activityContext);
    }
}