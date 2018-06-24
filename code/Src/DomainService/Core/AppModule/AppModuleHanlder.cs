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
        private readonly INewsDa newsDa;

        public AppModuleHanlder(IAppModuleDA appModule, IUtil util, INewsDa newsDa)
        {
            this.appModule = appModule;
            this.util = util;
            this.newsDa = newsDa;
        }

        public AppModuleResponse Process(GetAppModuleRequest request)
        {
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
            if (request.AppId <= 0)
                throw new TransactionException("01", "appId 不允许为空");
            if (request.ModuleType<0)
            {
                throw new TransactionException("01", "module Type不允许为空");
            }
            //查询数据库
            var modules = appModule.GetAppModule(request.AppId,request.ModuleType);
            var response=new AppModuleResponse()
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
            //foreach (var module in response.Modules)
            //{
            //    var news = newsDa.GetModuleNews(module.ModuleId);
            //    module.News = news.Select(x => new NewsInfo()
            //    {
            //        NewsId = x.NewsId,
            //        NewsImg = x.ImageUrl,
            //        NewsName = x.Title,
            //        CreateTime = x.CreateTime,
            //        Source = x.Source,
            //        Author = x.Author,
            //    }).ToList();
            //}
            return response;
        }
    }
}
