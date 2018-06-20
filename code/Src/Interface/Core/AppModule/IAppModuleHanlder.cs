using Eagles.Application.Model.AppModel.Module;
using Eagles.Base;

namespace Eagles.Interface.Core.AppModule
{
    public interface IAppModuleHanlder:IInterfaceBase
    {
        AppModuleResponse Process(GetAppModuleRequest request);
    }
}
