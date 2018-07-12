namespace Eagles.Application.Model.UserComment.GetUserComment
{
    /// <summary>
    /// 用户评论查询
    /// </summary>
    public class GetUserCommentRequest : RequestBase
    {
        /// <summary>
        /// 评论类型 0-任务 1-活动
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

        /// <summary>
        /// 评论状态 0-全部 1-审核通过
        /// </summary>
        public int CommentStatus { get; set; }
    }
}