using System;

namespace Eagles.Application.Model.AppModel.User.Token
{
    /// <summary>
    /// 注册接口
    /// </summary>
    public class TokenRequest : RequestBase
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 短信验证码
        /// </summary>
        public string SmsCode { get; set; }
    }
}