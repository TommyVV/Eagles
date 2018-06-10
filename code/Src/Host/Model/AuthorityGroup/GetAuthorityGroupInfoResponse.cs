using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.AuthorityGroup
{
    public class GetAuthorityGroupInfoResponse:ResponseBase
    {
        public AuthorityGroupInfo List { get; set; }
    }
}
