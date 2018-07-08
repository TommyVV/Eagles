
namespace Eagles.Application.Model.Task.GetTask
{
    /// <summary>
    /// 任务查询
    /// </summary>
    public class GetTaskRequest : QueryRequestBase
    {
        /// <summary>
        /// 任务状态
        /// </summary>
        public string Status { get; set; }
    }
}