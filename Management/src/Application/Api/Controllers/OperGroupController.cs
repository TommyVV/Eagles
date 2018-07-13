using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.AuthorityGroup.Requset;
using Eagles.Application.Model.AuthorityGroup.Response;
using Eagles.Application.Model.AuthorityGroupSetUp.Requset;
using Eagles.Application.Model.AuthorityGroupSetUp.Response;
using Eagles.Application.Model.Operator.Requset;
using Eagles.Application.Model.Operator.Response;
using Eagles.Base;
using Eagles.Interface.Core;

using Eagles.Application.Host.Common;
namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// Operator
    /// </summary>
    public class OperGroupController : ApiController
    {
        private readonly IOperGroupHandler _OperatorHandler;

        public OperGroupController(IOperGroupHandler testHandler)
        {
            this._OperatorHandler = testHandler;
        }


        /// <summary>
        /// 编辑 管理员群组
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> EditOperGroup(EditAuthorityGroupRequset requset)
        {
             return ApiActuator.Runing(requset, (requset1) =>_OperatorHandler.EditOperGroup(requset));
        }

        /// <summary>
        /// 删除 管理员群组
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> RemoveOperGroup(RemoveAuthorityGroupInfoRequset requset)
        {
             return ApiActuator.Runing(requset, (requset1) =>_OperatorHandler.RemoveOperGroup(requset));
        }

        /// <summary>
        /// 管理员群组 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetAuthorityGroupDetailResponse> GetOperGroupDetail(GetAuthorityGroupDetailRequset requset)
        {
             return ApiActuator.Runing(requset, (requset1) =>_OperatorHandler.GetOperGroupDetail(requset));
        }

        /// <summary>
        /// 管理员群组 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetAuthorityGroupResponse> GetOperGroupList(GetAuthorityGroupRequset requset)
        {
             return ApiActuator.Runing(requset, (requset1) =>_OperatorHandler.GetOperGroupList(requset));
        }


        /// <summary>
        /// 管理员群组功能 
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetAuthorityGroupSetUpResponse> GetAuthorityGroupSetUp(GetAuthorityGroupSetUpRequest requset)
        {
             return ApiActuator.Runing(requset, (requset1) =>_OperatorHandler.GetAuthorityGroupSetUp(requset));
        }

        /// <summary>
        /// 管理员群组功能 编辑
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> EditAuthorityGroupSetUp(EditAuthorityGroupSetUpRequset requset)
        {
             return ApiActuator.Runing(requset, (requset1) =>_OperatorHandler.EditAuthorityGroupSetUp(requset));
        }
    }
}