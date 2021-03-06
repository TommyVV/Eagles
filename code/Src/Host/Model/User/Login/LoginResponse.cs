﻿namespace Eagles.Application.Model.User.Login
{
    /// <summary>
    /// 登录接口
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 加密用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 是否是内部用户
        /// </summary>
        public int IsInternalUser { get; set; }

        /// <summary>
        /// 是否需要验证码
        /// </summary>
        public bool IsVerifyCode { get; set; }

        /// <summary>
        /// Token过期时间
        /// </summary>
        public string TokenExpTime { get; set; }
    }
}