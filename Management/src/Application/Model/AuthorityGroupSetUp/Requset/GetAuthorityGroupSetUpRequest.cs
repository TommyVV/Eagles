using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.AuthorityGroupSetUp.Requset
{
    public class GetAuthorityGroupSetUpRequest : OrgRequestBase
    {
        /// <summary>
        /// 权限组编号
        /// </summary>
        public int GroupId { get; set; }
    }
}
