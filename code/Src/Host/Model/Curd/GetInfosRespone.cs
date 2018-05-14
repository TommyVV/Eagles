using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    public class GetInfosRespone : ResponseBase
    {
        public int TotalCount { get; set; }
        public List<UserInfo> List { get; set; }
    }
}
