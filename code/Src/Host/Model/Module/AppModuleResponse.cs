using System.Collections.Generic;

namespace Eagles.Application.Model.Module
{
    public class AppModuleResponse:ResponseBase
    {
        public List<Module> Modules { get; set; }
    }
}
