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
        public int CommentId { get; set; }

        /// <summary>
        /// 活动/任务Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string CommentContent { get; set; }

        /// <summary>
        /// 评论用户
        /// </summary>
        public int CommentUserId { get; set; }

        /// <summary>
        /// 评论用户名称
        /// </summary>
        public string CommentUserName { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime CommentTime { get; set; }
    }
}