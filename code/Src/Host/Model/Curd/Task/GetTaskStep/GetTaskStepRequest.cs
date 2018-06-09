using System;

namespace Eagles.Application.Model.Curd.Task.GettaskStep
{
    /// <summary>
    /// 任务步骤查询
    /// </summary>
    public class GetTaskStepRequest : RequestBase
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        public string TaskId { get; set; }
    }
}