using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.PartyMember
{
    public class AlterAuthorityUserSetUpRequset
    {
        

        /// <summary>
        /// 下级
        /// </summary>
        public List<int> UserIds { get; set; }

        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }
    }
}
