using System;

namespace Eagles.Application.Model.Common
{
    /// <summary>
    /// 新闻文章类
    /// </summary>
    public class News
    {
        /// <summary>
        /// 新闻文章Id
        /// </summary>
        public int NewsId { get; set; }

        /// <summary>
        /// 新闻文章标题
        /// </summary>
        public string NewsTitle { get; set; }

        /// <summary>
        /// 文章类型 00:文章 01:心得体会 02:会议
        /// </summary>
        public string NewsType { get; set; }

        /// <summary>
        /// 新闻文章内容
        /// </summary>
        public string NewsContent { get; set; }

        /// <summary>
        /// 新闻文章日期
        /// </summary>
        public DateTime NewsDate { get; set; }
        
    }
}
