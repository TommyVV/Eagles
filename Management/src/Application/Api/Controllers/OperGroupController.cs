using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.AuthorityGroup.Requset;
using Eagles.Application.Model.AuthorityGroup.Response;
using Eagles.Application.Model.AuthorityGroupSetUp.Requset;
using Eagles.Application.Model.AuthorityGroupSetUp.Response;
using Eagles.Application.Model.Operator.Requset;
using Eagles.Application.Model.Operator.Response;
using Eagles.Interface.Core;

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
        public ResponseBase EditOperGroup(EditAuthorityGroupRequset requset)
        {
            return _OperatorHandler.EditOperGroup(requset);
        }

        /// <summary>
        /// 删除 管理员群组
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseBase RemoveOperGroup(RemoveAuthorityGroupInfoRequset requset)
        {
            return _OperatorHandler.RemoveOperGroup(requset);
        }

        /// <summary>
        /// 管理员群组 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public GetAuthorityGroupDetailResponse GetOperGroupDetail(GetAuthorityGroupDetailRequset requset)
        {
            return _OperatorHandler.GetOperGroupDetail(requset);
        }

        /// <summary>
        /// 管理员群组 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public GetAuthorityGroupResponse GetOperGroupList(GetAuthorityGroupRequset requset)
        {
            return _OperatorHandler.GetOperGroupList(requset);
        }


        /// <summary>
        /// 管理员群组功能 
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public GetAuthorityGroupSetUpResponse GetAuthorityGroupSetUp(GetAuthorityGroupSetUpRequest requset)
        {
            return _OperatorHandler.GetAuthorityGroupSetUp(requset);
        }

        /// <summary>
        /// 管理员群组功能 编辑
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseBase EditAuthorityGroupSetUp(EditAuthorityGroupSetUpRequset requset)
        {
            return _OperatorHandler.EditAuthorityGroupSetUp(requset);
        }
    }
}