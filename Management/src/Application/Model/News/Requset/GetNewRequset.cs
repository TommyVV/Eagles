using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.News.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetNewRequset : OrgListRequestBase
    {
        /// <summary>
        /// 作者id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 新闻名字
        /// </summary>
        public string  NewsName { get; set; }

        /// <summary>
        /// 新闻类型
        /// </summary>
        public NewsType NewsType { get; set; }
    }
}
