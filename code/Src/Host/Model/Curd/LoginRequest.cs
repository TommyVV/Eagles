using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginRequest : RequestBase
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }

        public string UserPwd { get; set; }

        public string VerifyCode { get; set; }
    }
}
