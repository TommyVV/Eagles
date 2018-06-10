using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.PartyMember
{
    public class AlterUserInfoDetailsRequest
    {
        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 集合
        /// </summary>
        public UserInfoDetails List { get; set; }
    }
}
