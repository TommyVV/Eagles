using System;

namespace Eagles.Application.Model.Common
{
    /// <summary>
    /// 评论
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// 评论Id
        /// </summary>
        public string CommentId { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string CommentContent { get; set; }

        /// <summary>
        /// 评论用户
        /// </summary>
        public string CommentUserId { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime CommentTime { get; set; }
    }
}