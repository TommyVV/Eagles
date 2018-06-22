using System.Collections.Generic;

namespace Eagles.Application.Model.AuthorityGroup.Response
{
    /// <summary>
    /// 权限组列表返回
    /// </summary>
    public class GetAuthorityGroupResponse:ResponseBase
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<Model.AuthorityGroup> List { get; set; }
    }
}
