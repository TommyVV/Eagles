using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Organization
{
     public class OrganizationRequset:RequestBase
    {

        /// <summary>
        /// 机构ID
        /// </summary>
        public string OrgId { get; set; }

    }
}
