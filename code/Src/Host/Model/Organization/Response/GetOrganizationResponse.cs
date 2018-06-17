using System.Collections.Generic;

namespace Eagles.Application.Model.Organization.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetOrganizationResponse : ResponseBase
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<Model.Organization> List { get; set; }
    }
}
