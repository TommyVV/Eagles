using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Login
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginResponse : ResponseBase
    {
        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }
    }
}
