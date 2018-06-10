using System;
using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Curd.Task.GetTask
{
    /// <summary>
    /// 任务查询
    /// </summary>
    public class GetTaskResponse : ResponseBase
    {
        /// <summary>
        /// 任务列表
        /// </summary>
        public List<Common.Task> TaskList { get; }
    }
}