using System;
using System.Web.Http;
using Eagles.Application.Model.Module;
using Eagles.Base;
using Eagles.Interface.Core.AppModule;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 栏目
    /// </summary>
    [ValidServiceToken]
    public class AppModuleController : ApiController
    {
        private readonly IAppModuleHanlder appModule;

        /// <summary>
        /// AppModuleController
        /// </summary>
        /// <param name="appModule"></param>
        public AppModuleController(IAppModuleHanlder appModule)
        {
            this.appModule = appModule;
        }

        /// <summary>
        /// 获取页面模块(分类)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<AppModuleResponse> GetModule(GetAppModuleRequest request)
        {
            return ApiActuator.Runing(() => appModule.Process(request));
        }
    }
}