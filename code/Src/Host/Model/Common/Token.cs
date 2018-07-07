using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.Application.Host.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class Token
    {
        /// <summary>
        /// 支部
        /// </summary>
        public int BranchId { get; set; }
        
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }
        /// <summary>
        /// 机构
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
    }
}