using System;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Curd.User.EditUser
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