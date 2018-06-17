using Eagles.Application.Model.Organization.Model;

namespace Eagles.Application.Model.Organization.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class EditOrganizationRequset:RequestBase
    {
        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public OrganizationDetail Info { get; set; }
    }
}
