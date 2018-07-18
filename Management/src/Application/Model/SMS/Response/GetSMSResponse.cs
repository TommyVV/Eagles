using System.Collections.Generic;
using Eagles.Application.Model.SMS.Model;

namespace Eagles.Application.Model.SMS.Response
{
    public class GetSMSResponse 
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<SMSInfo> List { get; set; }
    }
}
