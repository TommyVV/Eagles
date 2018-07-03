using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.Organization.Requset;
using Eagles.Application.Model.Organization.Response;
using Eagles.Base;
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
        [HttpPost]
        public ResponseFormat<bool> EditOrganization(EditOrganizationRequset requset)
        {
            return ApiActuator.Runing(() =>_columnHandler.EditOrganization(requset));
        }

        /// <summary>
        /// 删除 机构
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> RemoveOrganization(RemoveOrganizationRequset requset)
        {
            return ApiActuator.Runing(() =>_columnHandler.RemoveOrganization(requset));
        }

        /// <summary>
        /// 机构 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetOrganizationDetailResponse> GetOrganizationDetail(GetOrganizationDetailRequset requset)
        {
            return ApiActuator.Runing(() =>_columnHandler.GetOrganizationDetail(requset));
        }

        /// <summary>
        /// 机构 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetOrganizationResponse> GetOrganization(GetOrganizationRequset requset)
        {
            return ApiActuator.Runing(() =>_columnHandler.Organization(requset));
        }
    }
}