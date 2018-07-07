
using System.Collections.Generic;

namespace Eagles.Application.Model.Scroll.GetScrollNew
{
    /// <summary>
    /// 滚动消息查询
    /// </summary>
    public class GetScrollNewsResponse
    {
       public List<SystemNews> SystemNewsList { get; set; }

    }

    public class SystemNews
    {
        /// <summary>
        /// 消息Id
        /// </summary>
        public int NewsId { get; set; }

        /// <summary>
        /// 消息名称
        /// </summary>
        public string NewsName { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string NewsContent { get; set; }
    }
}