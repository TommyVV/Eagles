using System.Collections.Generic;
using Eagles.Application.Model.Audit.Model;

namespace Eagles.Application.Model.Audit.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAuditResponse : ResponseBase
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<Model.Audit> List { get; set; }
    }
}
