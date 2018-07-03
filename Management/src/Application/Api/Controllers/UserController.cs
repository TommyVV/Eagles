using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.PartyMember.Requset;
using Eagles.Application.Model.PartyMember.Response;
using Eagles.Base;
using Eagles.Interface.Core;

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
            return ApiActuator.Runing(() => _partyMemberHandler.EditUserInfoDetails(requset));
        }

        /// <summary>
        /// 删除 党员
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> RemovePartyMember(RemoveUserInfoDetailsRequest requset)
        {
            return ApiActuator.Runing(() => _partyMemberHandler.RemoveUserInfoDetails(requset));
        }

        /// <summary>
        /// 党员 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetUserInfoDetailResponse> GetPartyMemberDetail(GetUserInfoDetailRequest requset)
        {
            return ApiActuator.Runing(() => _partyMemberHandler.GetUserInfoDetail(requset));
        }

        /// <summary>
        /// 党员 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetPartyMemberResponse> GetPartyMember(GetPartyMemberRequest requset)
        {
            return ApiActuator.Runing(() => _partyMemberHandler.GetPartyMemberList(requset));
        }


        /// <summary>
        /// 权限 删除
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>]
        [HttpPost]
        public ResponseFormat<bool> RemoveAuthorityUserSetUp(RemoveAuthorityUserSetUp requset)
        {
            return ApiActuator.Runing(() => _partyMemberHandler.RemoveAuthorityUserSetUp(requset));
        }


        /// <summary>
        /// 权限 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetAuthorityUserSetUpResponse> GetAuthorityUserSetUp(GetAuthorityUserSetUpRequset requset)
        {
            return ApiActuator.Runing(() => _partyMemberHandler.GetAuthorityUserSetUp(requset));
        }


        /// <summary>
        /// 权限 创建
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> CreateAuthorityUserSetUp(CreateAuthorityUserSetUp requset)
        {
            return ApiActuator.Runing(() => _partyMemberHandler.CreateAuthorityUserSetUp(requset));
        }
    }
}