using System;

namespace Eagles.Application.Model.Curd.Task.RemoveTaskStep
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
    }
}