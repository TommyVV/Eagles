namespace Eagles.Application.Model.Task.GetTaskComment
{
    /// <summary>
    /// 任务评论查询
    /// </summary>
    public class GetTaskCommentRequest : RequestBase
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        public int TaskId { get; set; }
    }
}