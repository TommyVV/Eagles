using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.User.EditUser
{
    /// <summary>
    /// 用户信息修改
    /// </summary>
    public class EditUserRequest : RequestBase
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo RequestUserInfo { get; set; }
    }
}