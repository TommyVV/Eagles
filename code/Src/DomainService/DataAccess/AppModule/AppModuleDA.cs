using System;
using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.Interface.DataAccess.AppModule;

namespace Ealges.DomianService.DataAccess.AppModule
{
    public class AppModuleDA: IAppModuleDA
    {
        private readonly IDbManager dbManager;

        public AppModuleDA(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<Eagles.DomainService.Model.App.TbAppModule> GetAppModule(int orgId, int moduleType)
        {
            var result=dbManager.Query<Eagles.DomainService.Model.App.TbAppModule>(@"SELECT OrgId,
    ModuleId,
    ModuleName,
    TragetUrl,
    ModuleType,
    SmallImageUrl,
    ImageUrl,
    Priority,
    IndexPageCount,
    IndexDisplay
FROM eagles.tb_app_module where OrgId=@OrgId And ModuleType=@ModuleType order by Priority desc ", new {OrgId = orgId, ModuleType =moduleType});
            return result;
        }
    }
}
