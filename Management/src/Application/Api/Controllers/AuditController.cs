using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.Audit.Requset;
using Eagles.Application.Model.Audit.Response;
using Eagles.Interface.Core;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditController : ApiController
    {
        private readonly IAuditHandler _AuditHandler;

        public AuditController(IAuditHandler testHandler)
        {
            this._AuditHandler = testHandler;
        }



        /// <summary>
        /// 创建 审核
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/CreateAudit")]
        [HttpPost]
        public ResponseBase CreateAudit(CreateAuditRequset requset)
        {
            return _AuditHandler.CreateAudit(requset);
        }

 

        /// <summary>
        /// 审核 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/GetAudit")]
        [HttpPost]
        public GetAuditResponse GetAudit(GetAuditRequest requset)
        {
            return _AuditHandler.GetAuditList(requset);
        }
    }
}