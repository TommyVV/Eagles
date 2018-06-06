using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    class TaskQueryRequest : RequestBase
    {
        public string Token { get; set; }

        public string UserId { get; set; }
    }
}
