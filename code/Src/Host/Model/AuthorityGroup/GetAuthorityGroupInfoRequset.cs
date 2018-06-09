using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.AuthorityGroup
{
    public class GetAuthorityGroupInfoRequset
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int AuthorityGroupId { get; set; }

        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }
    }
}
