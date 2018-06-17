using System;
using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Curd.Activity.GetActivityComment
{
    /// <summary>
    /// 活动评论查询
    /// </summary>
    public class GetActivityCommentResponse : ResponseBase
    {
        /// <summary>
        /// 活动评论
        /// </summary>
        public List<Comment> ActivityCommentList { get; set; }
    }
}