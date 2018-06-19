using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.Organization.Requset;
using Eagles.Application.Model.Organization.Response;
using Eagles.Interface.Core;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class OrganizationController : ApiController
    {
        private readonly IOrganizationHandler _columnHandler;

        public OrganizationController(IOrganizationHandler testHandler)
        {
            this._columnHandler = testHandler;
        }



        /// <summary>
        /// 编辑 机构
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/EditOrganization")]
        [HttpPost]
        public ResponseBase EditOrganization(EditOrganizationRequset requset)
        {
            return _columnHandler.EditOrganization(requset);
        }

        /// <summary>
        /// 删除 机构
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/RemoveOrganization")]
        [HttpPost]
        public ResponseBase RemoveOrganization(RemoveOrganizationRequset requset)
        {
            return _columnHandler.RemoveOrganization(requset);
        }

        /// <summary>
        /// 机构 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/GetOrganizationDetail")]
        [HttpPost]
        public GetOrganizationDetailResponse GetOrganizationDetail(GetOrganizationDetailRequset requset)
        {
            return _columnHandler.GetOrganizationDetail(requset);
        }

        /// <summary>
        /// 机构 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/GetOrganization")]
        [HttpPost]
        public GetOrganizationResponse GetOrganization(GetOrganizationRequset requset)
        {
            return _columnHandler.Organization(requset);
        }
    }
}