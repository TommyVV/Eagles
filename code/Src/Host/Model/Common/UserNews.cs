using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Common
{
    /// <summary>
    /// 用户文章类
    /// </summary>
    public class UserNews
    {
        /// <summary>
        /// 新闻文章Id
        /// </summary>
        public int NewsId { get; set; }

        /// <summary>
        /// 新闻文章Id
        /// </summary>
        public NewsEnum NewsType { get; set; }

        /// <summary>
        /// 新闻文章标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文章时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string NewsContent { get; set; }
        
        /// <summary>
        /// 阅读数量
        /// </summary>
        public int ViewsCount { get; set; }

        /// <summary>
        /// 党员
        /// </summary>
        public int ToUser { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public int IsPublic { get; set; }
    }
}