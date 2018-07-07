namespace Eagles.Application.Model.UserComment.EditUserComment
{
    /// <summary>
    /// 用户评论接口
    /// </summary>
    public class EditUserCommentRequest : RequestBase
    {
        /// <summary>
        /// 评论类型 0-任务 1-活动 2-
        /// </summary>
        public string CommentType { get; set; }

        /// <summary>
        /// 活动/任务Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 评论用户Id
        /// </summary>

        public int CommentUserId { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Comment { get; set; }
        
    }
}