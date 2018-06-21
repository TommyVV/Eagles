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
        public string Title { get; set; }

        /// <summary>
        /// 新闻文章内容
        /// </summary>
        public DateTime CreateTime { get; set; }
        
        /// <summary>
        /// 缩略图
        /// </summary>
        public string ImageUrl { get; set; }
        
    }
}