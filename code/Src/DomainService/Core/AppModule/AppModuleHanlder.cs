using System.Linq;
using Eagles.Application.Model.Module;
using Eagles.Base;
using Eagles.Interface.Core.AppModule;
using Eagles.Interface.DataAccess.AppModule;
using Eagles.Interface.DataAccess.NewsDa;
using Eagles.Interface.DataAccess.Util;
using Module = Eagles.Application.Model.Module.Module;

namespace Eagles.DomainService.Core.AppModule
{
    public class AppModuleHanlder: IAppModuleHanlder
    {
        private readonly IAppModuleDA appModule;
        private readonly IUtil util;

        public AppModuleHanlder(IAppModuleDA appModule, IUtil util)
        {
            this.appModule = appModule;
            this.util = util;
        }

        public AppModuleResponse Process(GetAppModuleRequest request)
        {
            if (request.AppId <= 0)
                throw new TransactionException("01", "AppId不允许为空");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
            if (request.ModuleType<0)
            {
                throw new TransactionException("01", "module Type不允许为空");
            }
            //查询数据库
            var modules = appModule.GetAppModule(request.AppId,(int)request.ModuleType);
            if (request.ModuleType == 0)
            {
                modules = modules.Where(x => x.IndexDisplay == 1).ToList();
            }
            var response=new AppModuleResponse()
            {
                Modules = modules.Select(x => new Module()
                {
                    ImageUrl = x.ImageUrl,
                    IndexPageCount = x.IndexPageCount,
                    ModuleId = x.ModuleId,
                    ModuleName = x.ModuleName,
                    Priority = x.Priority,
                    SmallImageUrl = x.SmallImageUrl,
                    TargetUrl = x.TargetUrl
                }).ToList()
            };
            return response;
        }
    }
}
