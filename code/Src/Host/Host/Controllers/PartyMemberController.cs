using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.PartyMember.Requset;
using Eagles.Application.Model.PartyMember.Response;
using Eagles.Interface.Core;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PartyMemberController : ApiController
    {
        private readonly IPartyMemberHandler _partyMemberHandler;

        public PartyMemberController(IPartyMemberHandler testHandler)
        {
            this._partyMemberHandler = testHandler;
        }



        /// <summary>
        /// 编辑 党员
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/EditUserInfo")]
        [HttpPost]
        public ResponseBase EditPartyMember(EditUserInfoDetailsRequest requset)
        {
            return _partyMemberHandler.EditUserInfoDetails(requset);
        }

        /// <summary>
        /// 删除 党员
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/RemoveUserInfo")]
        [HttpPost]
        public ResponseBase RemovePartyMember(RemoveUserInfoDetailsRequest requset)
        {
            return _partyMemberHandler.RemoveUserInfoDetails(requset);
        }

        /// <summary>
        /// 党员 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/GetUserInfoDetail")]
        [HttpPost]
        public GetUserInfoDetailResponse GetPartyMemberDetail(GetUserInfoDetailRequest requset)
        {
            return _partyMemberHandler.GetUserInfoDetail(requset);
        }

        /// <summary>
        /// 党员 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/GetUserInfoList")]
        [HttpPost]
        public GetPartyMemberResponse GetPartyMember(GetPartyMemberRequest requset)
        {
            return _partyMemberHandler.GetPartyMemberList(requset);
        }


        /// <summary>
        /// 权限 删除
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/RemoveAuthorityUserSetUp")]
        [HttpPost]
        public ResponseBase RemoveAuthorityUserSetUp(RemoveAuthorityUserSetUp requset)
        {
            return _partyMemberHandler.RemoveAuthorityUserSetUp(requset);
        }


        /// <summary>
        /// 权限 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/GetAuthorityUserSetUpList")]
        [HttpPost]
        public GetAuthorityUserSetUpResponse GetAuthorityUserSetUp(GetAuthorityUserSetUpRequset requset)
        {
            return _partyMemberHandler.GetAuthorityUserSetUp(requset);
        }


        /// <summary>
        /// 权限 创建
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/CreateAuthorityUserSetUp")]
        [HttpPost]
        public ResponseBase CreateAuthorityUserSetUp(CreateAuthorityUserSetUp requset)
        {
            return _partyMemberHandler.CreateAuthorityUserSetUp(requset);
        }
    }
}