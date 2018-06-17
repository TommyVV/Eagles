using System;

namespace Eagles.Application.Model.Curd.Activity.EditActivityComment
{
    /// <summary>
    /// 活动评论接口
    /// </summary>
    public class EditActivityCommentRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        
        /// <summary>
        /// 活动Id
        /// </summary>
        public int ActivityId { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 评论用户Id
        /// </summary>
        public int CommentUserId { get; set; }
    }
}