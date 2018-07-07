using System.Collections.Generic;

namespace Eagles.Application.Model.News.GetNews
{
    /// <summary>
    /// 文章列表查询
    /// </summary>
    public class GetNewsResponse 
    {
        /// <summary>
        /// NewsList
        /// </summary>
        public List<Common.News> NewsList { get; set; }
    }
}