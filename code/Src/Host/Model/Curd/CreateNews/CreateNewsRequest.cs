using System;
using Eagles.Application.Model.Curd.Enum;

namespace Eagles.Application.Model.Curd.CreateNews
{
    /// <summary>
    /// 新闻文章发布
    /// </summary>
    public class CreateNewsRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

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