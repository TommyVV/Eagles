namespace Eagles.Application.Model.Task.GetTask
{
    /// <summary>
    /// 任务查询
    /// </summary>
    public class GetTaskRequest : RequestBase
    {
        /// <summary>
        /// 加密用户编号
        /// </summary>
        public string EncryptUserid { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public int Status { get; set; }
    }
}