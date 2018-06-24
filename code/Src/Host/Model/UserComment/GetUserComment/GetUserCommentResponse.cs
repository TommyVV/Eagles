using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.UserComment.GetUserComment
{
    /// <summary>
    /// 用户评论查询
    /// </summary>
    public class GetUserCommentResponse : ResponseBase
    {
        /// <summary>
        /// 活动评论
        /// </summary>
        public List<Comment> CommentList { get; set; }
    }
}