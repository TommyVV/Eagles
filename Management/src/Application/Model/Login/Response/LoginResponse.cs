namespace Eagles.Application.Model.Login.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginResponse 
    {
        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 是否需要验证码验证
        /// </summary>
        public bool IsVerificationCode { get; set; }
    }
}
