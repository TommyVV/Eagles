using System.Web.Http;
using Eagles.Application.Model.PartyMember.Requset;
using Eagles.Application.Model.PartyMember.Response;
using Eagles.Interface.Core;
using Eagles.Application.Host.Common;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class UserController : ApiController
    {
        private readonly IUserHandler _partyMemberHandler;

        public UserController(IUserHandler testHandler)
        {
            this._partyMemberHandler = testHandler;
        }

        /// <summary>
        /// 编辑 党员
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> EditPartyMember(EditUserInfoDetailsRequest requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _partyMemberHandler.EditUserInfoDetails(requset));
        }

        /// <summary>
        /// 删除 党员
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> RemovePartyMember(RemoveUserInfoDetailsRequest requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _partyMemberHandler.RemoveUserInfoDetails(requset));
        }

        /// <summary>
        /// 党员 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetUserInfoDetailResponse> GetPartyMemberDetail(GetUserInfoDetailRequest requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _partyMemberHandler.GetUserInfoDetail(requset));
        }

        /// <summary>
        /// 党员 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetPartyMemberResponse> GetPartyMember(GetPartyMemberRequest requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _partyMemberHandler.GetPartyMemberList(requset));
        }


        /// <summary>
        /// 党员关系删除
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>]
        [HttpPost]
        public ResponseFormat<bool> RemoveAuthorityUserSetUp(RemoveAuthorityUserSetUp requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _partyMemberHandler.RemoveAuthorityUserSetUp(requset));
        }


        /// <summary>
        /// 查询用户下级列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetAuthorityUserSetUpResponse> GetAuthorityUserSetUp(GetAuthorityUserSetUpRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _partyMemberHandler.GetAuthorityUserSetUp(requset));
        }


        /// <summary>
        /// 用户上下级关系创建
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> CreateAuthorityUserSetUp(CreateAuthorityUserSetUp requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _partyMemberHandler.CreateAuthorityUserSetUp(requset));
        }

        /// <summary>
        /// 党员导入
        /// </summary>
        /// <returns></returns>
        public ResponseFormat<ImportUserResponse> BatchImportUser(ImportUserRequest request)
        {

            return ApiActuator.Runing(request, (requset1) => _partyMemberHandler.BatchImportUser(request));
           
        }
    }
}