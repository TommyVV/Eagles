using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Operator
{
    public class OperatorInfoDetails: OperatorInfo
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
        /// 权限组id
        /// </summary>
        public int AuthorityGroupId { get; set; }
    }
}
