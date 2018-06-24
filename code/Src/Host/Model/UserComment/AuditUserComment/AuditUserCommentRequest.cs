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
        public int Id { get; set; }
        
    }
}