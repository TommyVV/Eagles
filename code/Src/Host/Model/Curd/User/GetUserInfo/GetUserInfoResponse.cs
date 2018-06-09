using System;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Curd.User.GetUserInfo
{
    /// <summary>
    /// 用户信息查询
    /// </summary>
    public class GetUserInfoResponse : ResponseBase
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo ResultUserInfo { get; set; }
    }
}