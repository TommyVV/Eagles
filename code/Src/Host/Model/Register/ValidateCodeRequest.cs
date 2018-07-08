namespace Eagles.Application.Model.Register
{
    public class ValidateCodeRequest:RequestBase
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
    }
}
