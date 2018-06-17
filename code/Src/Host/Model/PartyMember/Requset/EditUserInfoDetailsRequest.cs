using Eagles.Application.Model.PartyMember.Model;

namespace Eagles.Application.Model.PartyMember.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class EditUserInfoDetailsRequest:RequestBase
    {
        /// <summary>
        /// 集合
        /// </summary>
        public UserInfoDetails List { get; set; }
    }
}
