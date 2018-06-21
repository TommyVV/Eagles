﻿using System;

namespace Eagles.Application.Model.AppModel.Task.GetTaskDetail
{
    /// <summary>
    /// 任务详情查询
    /// </summary>
    public class GetTaskDetailRequest : RequestBase
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        public int TaskId { get; set; }
    }
}