using Eagles.Application.Model.ActivityTask.Model;

namespace Eagles.Application.Model.ActivityTask.Requset
{
    /// <summary>
    /// 活动 修改/新增
    /// </summary>
    public class EditActivityTaskInfoRequset : RequestBase
    {
        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public ActivityDetailInfo DetailInfo { get; set; }

        /// <summary>
        ///  支部id
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        ///  机构id
        /// </summary>
        public int OrgId { get; set; }
        
    }
}
