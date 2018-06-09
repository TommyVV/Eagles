using System;

namespace Eagles.Application.Model.Curd.Task.RemoveTaskStep
{
    /// <summary>
    /// 任务步骤删除
    /// </summary>
    public class RemoveTaskStepRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 任务Id
        /// </summary>
        public string TaskId { get; set; }
    }
}