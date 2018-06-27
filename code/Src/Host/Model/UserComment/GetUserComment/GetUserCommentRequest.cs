namespace Eagles.Application.Model.UserComment.GetUserComment
{
    /// <summary>
    /// 用户评论查询
    /// </summary>
    public class GetUserCommentRequest : RequestBase
    {
        /// <summary>
        /// 评论类型 0-活动 1-任务
        /// </summary>
        public string CommentType { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 查询当前评论用户Id
        /// </summary>
        public int UserId { get; set; }
    }
}