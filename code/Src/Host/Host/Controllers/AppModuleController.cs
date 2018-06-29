using System;
using System.Web.Http;
using Eagles.Application.Model.Module;
using Eagles.Base;
using Eagles.Interface.Core.AppModule;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// AppModuleController
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
        public AppModuleResponse GetModule(GetAppModuleRequest request)
        {
            try
            {
                return appModule.Process(request);
            }
            catch (TransactionException e)
            {
                return new AppModuleResponse()
                {
                    Code = e.ErrorCode,
                    Message = e.ErrorMessage
                };
            }
            catch (Exception e)
            {
                return new AppModuleResponse()
                {
                    Code = "99",
                    Message = "系统错误"
                };
            }
        }
    }
}