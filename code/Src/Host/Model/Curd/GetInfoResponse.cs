using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    public class GetInfoResponse : ResponseBase
    {
        public UserInfo UserInfo { get; set; }
    }
}
