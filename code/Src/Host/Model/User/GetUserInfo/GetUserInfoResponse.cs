using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.User.GetUserInfo
{
    /// <summary>
    /// 用户信息查询
    /// </summary>
    public class GetUserInfoResponse 
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo ResultUserInfo { get; set; }
    }
}