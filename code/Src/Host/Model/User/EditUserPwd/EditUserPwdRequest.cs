
namespace Eagles.Application.Model.User.EditUserPwd
{
    /// <summary>
    /// 用户密码修改
    /// </summary>
    public class EditUserPwdRequest : RequestBase
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd { get; set; }

        /// <summary>
        /// 用户新密码
        /// </summary>
        public string NewPwd { get; set; }
    }
}