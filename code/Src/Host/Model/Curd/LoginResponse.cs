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
    public class LoginResponse : ResponseBase
    {
        public string Token { get; set; }
    }
}
