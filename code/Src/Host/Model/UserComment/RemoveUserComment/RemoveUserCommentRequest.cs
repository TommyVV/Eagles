namespace Eagles.Application.Model.UserComment.RemoveUserComment
{
    /// <summary>
    /// 用户评论接口
    /// </summary>
    public class RemoveUserCommentRequest : RequestBase
    {
        /// <summary>
        /// 活动/任务Id
        /// </summary>
        public int MessageId { get; set; }

        /// <summary>
        /// 活动/任务Id
        /// </summary>
        public int Id { get; set; }
    }
}