using System.Collections.Generic;
using Eagles.Application.Model.Enums;
using Eagles.Application.Model.PartyMember.Model;

namespace Eagles.Application.Model.PartyMember.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckUserInfoDetailsResponse:ResponseBase
    {
        public List<UserInfoResult> list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CheckResult Result { get; set; }
    }
}
