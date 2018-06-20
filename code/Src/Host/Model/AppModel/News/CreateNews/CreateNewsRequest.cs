using Eagles.Application.Model.AppModel.Enum;

namespace Eagles.Application.Model.AppModel.News.CreateNews
{
    /// <summary>
    /// 新闻文章发布
    /// </summary>
    public class CreateNewsRequest : RequestBase
    {
        /// <summary>
        /// 新闻文章标题
        /// </summary>
        public string NewsTitle { get; set; }

        /// <summary>
        /// 文章类型 00:文章 01:心得体会 02:会议
        /// </summary>
        public NewsEnum NewsType { get; set; }

        /// <summary>
        /// 新闻文章内容
        /// </summary>
        public string NewsContent { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public string IsPublic { get; set; }
    }
}