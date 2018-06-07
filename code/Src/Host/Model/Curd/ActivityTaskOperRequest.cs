using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    class ActivityTaskOperRequest : RequestBase
    {
        public string Action { get; set; }

        public string ActivityTaskContent { get; set; }
    }
}
