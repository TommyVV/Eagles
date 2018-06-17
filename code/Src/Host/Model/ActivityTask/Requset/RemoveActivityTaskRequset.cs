namespace Eagles.Application.Model.ActivityTask.Requset
{
    /// <summary>
    /// 删除 活动任务 
    /// </summary>
    public class RemoveActivityTaskRequset : RequestBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string ActivityTaskId { get; set; }

    }
}
