using Eagles.Application.Model.Branch.Model;
using Eagles.Application.Model.Organization.Model;

namespace Eagles.Application.Model.Branch.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class EditBranchRequset : RequestBase
    {
        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public BranchDetail Info { get; set; }
    }
}
