﻿namespace Eagles.Application.Model.Task.GetTaskStep
{
    /// <summary>
    /// 任务步骤查询
    /// </summary>
    public class GetTaskStepRequest : RequestBase
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        public int TaskId { get; set; }
    }
}