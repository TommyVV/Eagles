
namespace Eagles.Application.Model.User.Register
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
        /// 注册密码
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 短信验证码
        /// </summary>
        public int ValidCode { get; set; }
        
        /// <summary>
        /// 验证码序号
        /// </summary>
        public int Seq { get; set; }
    }
}