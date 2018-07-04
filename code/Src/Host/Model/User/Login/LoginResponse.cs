namespace Eagles.Application.Model.User.Login
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
        /// 是否需要验证码
        /// </summary>
        public bool IsVerifyCode { get; set; }
    }
}