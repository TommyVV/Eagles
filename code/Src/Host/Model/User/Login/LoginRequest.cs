﻿
namespace Eagles.Application.Model.User.Login
{
    /// <summary>
    /// 登录接口
    /// </summary>
    public class LoginRequest : RequestBase
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string VerifyCode { get; set; }
    }
}