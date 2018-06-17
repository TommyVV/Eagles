using System.Collections.Generic;

namespace Eagles.Application.Model.News.Response
{
    public class GetNewsResponse : ResponseBase
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<Model.News> List { get; set; }
    }
}
