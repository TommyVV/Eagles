using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.SMS.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class GetSMSRequest : OrgListRequestBase
    {
        /// <summary>
        /// 短信提供方名称
        /// </summary>
        public string VendorName { get; set; }

    }
}
