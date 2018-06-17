namespace Eagles.Application.Model.ActivityTask.Requset
{
    /// <summary>
    /// 修改/新增 任务活动实体类
    /// </summary>
    public class EditActivityTaskInfoRequset : RequestBase
    {
        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public Model.ActivityTaskModel Info { get; set; }
    }
}
