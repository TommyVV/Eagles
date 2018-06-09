using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.PartyMember
{
    public class GetUserInfoDetailsResponse:ResponseBase
    {
        /// <summary>
        /// 集合
        /// </summary>
        public UserInfoDetails List { get; set; }
    }
}
