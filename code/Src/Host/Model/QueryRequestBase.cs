
namespace Eagles.Application.Model
{
    /// <summary>
    /// 查询基本的凭证
    /// </summary>
    public class QueryRequestBase
    {
        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }


        /// <summary>
        /// AppId
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; } = 1;
    }
}