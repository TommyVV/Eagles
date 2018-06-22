using System.Collections.Generic;

namespace Eagles.Application.Model.SMSOrg.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetSMSOrgResponse : ResponseBase
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<Model.SMSOrg> List { get; set; }
    }
}
