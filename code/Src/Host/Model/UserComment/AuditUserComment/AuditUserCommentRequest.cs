namespace Eagles.Application.Model.UserComment.AuditUserComment
{
    /// <summary>
    /// 用户评论接口
    /// </summary>
    public class AuditUserCommentRequest : RequestBase
    {
        /// <summary>
        /// 活动/任务Id
        /// </summary>
        public int CommentId { get; set; }

        /// <summary>
        /// 审核状态;0:通过;1:未通过
        /// </summary>
        public int ReviewStatus { get; set; }
    }
}