using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.SMS.Request
{
    public class RemoveSMSRequest : RequestBase
    {
        /// <summary>
        ///  '短信提供方ID',
        /// </summary>
        public int VendorId { get; set; }
    }
}
