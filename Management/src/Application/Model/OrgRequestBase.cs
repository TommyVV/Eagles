using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model
{
    /// <summary>
    /// 机构和组织
    /// </summary>
    public class OrgRequestBase : RequestBase
    {
        /// <summary>
        /// 机构id
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 支部id
        /// </summary>
        public int BranchId { get; set; }
    }

    /// <summary>
    /// 机构和组织
    /// </summary>
    public class OrgRequest 
    {
        /// <summary>
        /// 机构id
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 支部id
        /// </summary>
        public int BranchId { get; set; }
    }


    /// <summary>
    /// 机构和组织
    /// </summary>
    public class ORequest
    {
        /// <summary>
        /// 机构id
        /// </summary>
        public int OrgId { get; set; }

       
    }

}
