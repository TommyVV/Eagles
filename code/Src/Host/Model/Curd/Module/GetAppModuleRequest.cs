using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd.Module
{
    public class GetAppModuleRequest
    {
        public int ModuleType { get; set; }

        public int AppId { get; set; }
    }
}
