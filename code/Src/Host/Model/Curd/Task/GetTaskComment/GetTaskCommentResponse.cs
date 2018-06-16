using System;
using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Curd.Task.GetTaskComment
{
    /// <summary>
    /// 任务评论查询
    /// </summary>
    public class GetTaskCommentResponse : ResponseBase
    {
        /// <summary>
        /// 任务评论列表
        /// </summary>
        public List<Comment> TaskCommentList { get; set; }
    }
}