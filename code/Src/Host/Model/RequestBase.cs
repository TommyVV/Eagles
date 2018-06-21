namespace Eagles.Application.Model
{
    /// <summary>
    /// 基本的 凭证
    /// </summary>
    public class RequestBase
    {
        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }


        /// <summary>
        /// AppId
        /// </summary>
        public int AppId { get; set; }
    }
}
