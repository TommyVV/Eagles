using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.AuthorityGroupSetUp.Model;

namespace Eagles.Application.Model.AuthorityGroupSetUp.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAuthorityGroupSetUpResponse
    {
      
        /// <summary>
        /// 集合
        /// </summary>
        public List<AuthorityInfo> List { get; set; }
    }
}
