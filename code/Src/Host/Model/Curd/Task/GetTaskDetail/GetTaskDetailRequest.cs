using System;

namespace Eagles.Application.Model.Curd.Task.GetTaskDetail
{
    /// <summary>
    /// 任务详情查询
    /// </summary>
    public class GetTaskDetailRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 任务Id
        /// </summary>
        public int TaskId { get; set; }
    }
}