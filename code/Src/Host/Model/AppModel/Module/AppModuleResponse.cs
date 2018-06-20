using System.Collections.Generic;

namespace Eagles.Application.Model.AppModel.Module
{
    public class AppModuleResponse:ResponseBase
    {
        public List<Module> Modules { get; set; }
    }
}
