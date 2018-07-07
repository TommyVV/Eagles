
namespace Eagles.Application.Model.Task.GetTask
{
    /// <summary>
    /// 任务查询
    /// </summary>
    public class GetTaskRequest : RequestBase
    {
        /// <summary>
        /// 任务状态
        /// </summary>
        public string Status { get; set; }
    }
}