using Eagles.Application.Model.Curd.Module;
using Eagles.Base;

namespace Eagles.Interface.Core.AppModule
{
    public interface IAppModuleHanlder:IInterfaceBase
    {
        AppModuleResponse Process(GetAppModuleRequest request);
    }
}
