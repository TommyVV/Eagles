
namespace Eagles.Application.Model.AppModel.User.Register
{
    /// <summary>
    /// 注册接口
    /// </summary>
    public class RegisterRequest :RequestBase
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 短信验证码
        /// </summary>
        public int SmsCode { get; set; }
    }
}