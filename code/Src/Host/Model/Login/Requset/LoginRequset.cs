using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Login
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginRequset
    {
        /// <summary>
        /// 密码
        /// </summary>
        /// <returns></returns>
        public string Password { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        /// <returns></returns>
        public string Account { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string VerificationCode { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public LoginType LoginType { get; set; }

    }
}
