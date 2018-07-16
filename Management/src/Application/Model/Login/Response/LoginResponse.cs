namespace Eagles.Application.Model.Login.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginResponse 
    {
        /// <summary>
        /// 用户登录后的凭证，用于后续接口请求
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 是否需要验证码验证（暂时不用）
        /// </summary>
        public bool IsVerificationCode { get; set; }
    }
}
