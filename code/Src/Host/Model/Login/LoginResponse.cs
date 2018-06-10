using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Login
{
    public class LoginResponse:ResponseBase
    {
        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }
    }
}
