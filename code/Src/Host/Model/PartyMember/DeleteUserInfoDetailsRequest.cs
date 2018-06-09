using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.PartyMember
{
    public class DeleteUserInfoDetailsRequest
    {
        public int UserId { get; set; }
        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }
    }
}
