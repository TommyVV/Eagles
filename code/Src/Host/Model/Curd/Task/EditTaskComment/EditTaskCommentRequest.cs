using System;

namespace Eagles.Application.Model.Curd.Task.EditTaskComment
{
    /// <summary>
    /// 任务评论接口
    /// </summary>
    public class EditTaskCommentRequest : RequestBase
    {

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        
        /// <summary>
        /// 任务Id
        /// </summary>
        public int TaskId { get; set; }

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