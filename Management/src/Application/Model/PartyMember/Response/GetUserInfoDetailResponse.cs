using Eagles.Application.Model.PartyMember.Model;

namespace Eagles.Application.Model.PartyMember.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetUserInfoDetailResponse : ResponseBase
    {
        /// <summary>
        /// 集合
        /// </summary>
        public UserInfoDetails Info { get; set; }
    }
}
