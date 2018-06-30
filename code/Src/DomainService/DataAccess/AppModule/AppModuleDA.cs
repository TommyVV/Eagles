using System;
using System.Collections.Generic;
using System.Text;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.App;
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

        public List<TbAppModule> GetAppModule(int orgId, int moduleType)
        {
            var sql = new StringBuilder();
            sql .Append(@"SELECT OrgId,ModuleId,ModuleName,TargetUrl,ModuleType,SmallImageUrl,ImageUrl,Priority,IndexPageCount,IndexDisplay from eagles.tb_app_module where OrgId=@OrgId");
            if (moduleType > 0)
            {
                sql.Append(" And ModuleType=@ModuleType ");
            }
            sql.Append(" order by Priority desc ");
            var result=dbManager.Query<TbAppModule>(sql.ToString(), new {OrgId = orgId, ModuleType =moduleType});
            return result;
        }
    }
}
