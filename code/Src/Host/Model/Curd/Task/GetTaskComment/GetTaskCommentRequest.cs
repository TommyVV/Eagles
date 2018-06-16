using System;

namespace Eagles.Application.Model.Curd.Task.GetTaskComment
{
    /// <summary>
    /// 任务评论查询
    /// </summary>
    public class GetTaskCommentRequest : RequestBase
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        public int TaskId { get; set; }
    }
}