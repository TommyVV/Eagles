using System.Linq;
using Eagles.Application.Model.Curd.Module;
using Eagles.Base;
using Eagles.Interface.Core.AppModule;
using Eagles.Interface.DataAccess.AppModule;
using Module = Eagles.Application.Model.Curd.Module.Module;

namespace Eagles.DomainService.Core.AppModule
{
    public class AppModuleHanlder: IAppModuleHanlder
    {
        private readonly IAppModuleDA appModule;

        public AppModuleHanlder(IAppModuleDA appModule)
        {
            this.appModule = appModule;
        }

        public AppModuleResponse Process(GetAppModuleRequest request)
        {

            if (request.AppId<=0)
            {
                throw new TransactionException("01","appId 不允许为空");
            }

            if (request.ModuleType<0)
            {
                throw new TransactionException("01", "module Type不允许为空");
            }
            //查询数据库
            var modules = appModule.GetAppModule(request.AppId,request.ModuleType);
            return new AppModuleResponse()
            {
                Modules = modules.Select(x => new Module()
                {
                    ImageUrl = x.ImageUrl,
                    IndexDisplay = x.IndexDisplay == 1,
                    IndexPageCount = x.IndexPageCount,
                    ModuleId = x.ModuleId,
                    ModuleName = x.ModuleName,
                    Priority = x.Priority,
                    SmallImageUrl = x.SmallImageUrl,
                    TragetUrl = x.TragetUrl
                }).ToList()
            };
        }
    }
}
