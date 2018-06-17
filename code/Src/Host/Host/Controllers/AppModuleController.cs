using System;
using System.Web.Http;
using Eagles.Application.Model.Curd.Module;
using Eagles.Base;
using Eagles.Interface.Core.AppModule;

namespace Eagles.Host.Controllers
{
    [Route("api/app")]
    public class AppModuleController : ApiController
    {
        private readonly IAppModuleHanlder appModule;

        public AppModuleController(IAppModuleHanlder appModule)
        {
            this.appModule = appModule;
        }

        [HttpPost]
        [Route("GetModule")]
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
                    ErrorCode = e.ErrorCode,
                    Message = e.ErrorMessage
                };
            }
            catch (Exception e)
            {
                return new AppModuleResponse()
                {
                    ErrorCode = "99",
                    Message = "系统错误"
                };
            }
        }

       
    }
}