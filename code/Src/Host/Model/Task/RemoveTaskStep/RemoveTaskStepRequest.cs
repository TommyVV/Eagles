namespace Eagles.Application.Model.Task.RemoveTaskStep
{
    /// <summary>
    /// 任务步骤删除
    /// </summary>
    public class RemoveTaskStepRequest : RequestBase
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 步骤Id
        /// </summary>
        public int StepId { get; set; }
    }
}