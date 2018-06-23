namespace Eagles.Application.Model.Activity.EditActivityComment
{
    /// <summary>
    /// 活动评论接口
    /// </summary>
    public class EditActivityCommentRequest : RequestBase
    {
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