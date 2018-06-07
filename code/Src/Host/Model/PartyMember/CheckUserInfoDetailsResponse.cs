using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.PartyMember
{
    public class CheckUserInfoDetailsResponse:ResponseBase
    {
        public List<UserInfoResult> list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CheckResult Result { get; set; }
    }
}
