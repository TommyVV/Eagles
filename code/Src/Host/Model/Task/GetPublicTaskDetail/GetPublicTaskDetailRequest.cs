namespace Eagles.Application.Model.Task.GetPublicTaskDetail
{
    /// <summary>
    /// 公开任务详情查询
    /// </summary>
    public class GetPublicTaskDetailRequest : RequestBase
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        public int TaskId { get; set; }
    }
}