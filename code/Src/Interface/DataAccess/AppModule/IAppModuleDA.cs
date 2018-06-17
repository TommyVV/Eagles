using System.Collections.Generic;
using Eagles.Base;

namespace Eagles.Interface.DataAccess.AppModule
{
    public interface IAppModuleDA:IInterfaceBase
    {
        List<Eagles.DomainService.Model.App.AppModule> GetAppModule(int orgId, int moduleType);
    }
}
