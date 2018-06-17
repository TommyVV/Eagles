using System.Collections.Generic;

namespace Eagles.Application.Model.Curd.Module
{
    public class AppModuleResponse:ResponseBase
    {
        public List<Module> Modules { get; set; }
    }
}
