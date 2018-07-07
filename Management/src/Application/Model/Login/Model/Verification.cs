using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Login.Model
{
    public class Verification : OrgRequest
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }




    }
}
