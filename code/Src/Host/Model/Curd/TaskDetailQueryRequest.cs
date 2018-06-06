using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    class TaskDetailQueryRequest : RequestBase
    {
        public string Token { get; set; }

        public string TaskId { get; set; }
    }
}
