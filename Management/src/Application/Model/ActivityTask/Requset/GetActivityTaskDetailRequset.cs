namespace Eagles.Application.Model.ActivityTask.Requset
{
    /// <summary>
    /// 获取详情 任务/活动
    /// </summary>
    public class GetActivityTaskDetailRequset : RequestBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string ActivityId { get; set; }
    }
}
