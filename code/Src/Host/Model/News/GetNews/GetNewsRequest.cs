using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.News.GetNews
{
    /// <summary>
    /// 文章列表查询
    /// </summary>
    public class GetNewsRequest : RequestBase
    {
        /// <summary>
        /// 文章类型 00:文章 01:心得体会 02:会议
        /// </summary>
        public NewsEnum NewsType { get; set; }
    }
}