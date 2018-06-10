using System;

namespace Eagles.Application.Model.Curd.User.GetUserInfo
{
    /// <summary>
    /// 用户信息查询
    /// </summary>
    public class GetUserInfoRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }
}