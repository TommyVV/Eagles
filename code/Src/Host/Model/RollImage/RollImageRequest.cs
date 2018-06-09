using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.RollImage
{
    public class RollImageRequest:RequestBase
    {
        /// <summary>
        /// 机构号
        /// </summary>
        public int OrgId { get; set; }
    }
}
