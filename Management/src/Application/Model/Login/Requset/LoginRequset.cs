namespace Eagles.Application.Model.Login.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginRequset: ORequest
    {
        /// <summary>
        /// 密码
        /// </summary>
        /// <returns></returns>
        public string Password { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        /// <returns></returns>
        public string Account { get; set; }

        /// <summary>
        /// 验证码（暂时不用）
        /// </summary>
        public string VerificationCode { get; set; }

    }
}
