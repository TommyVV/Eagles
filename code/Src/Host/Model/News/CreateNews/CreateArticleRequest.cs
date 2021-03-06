﻿using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.News.CreateNews
{
    /// <summary>
    /// 文章发布
    /// </summary>
    public class CreateArticleRequest : RequestBase
    {
        /// <summary>
        /// 新闻文章标题
        /// </summary>
        public string NewsTitle { get; set; }

        /// <summary>
        /// 文章类型 00:文章 01:心得体会 02:会议 03:入党申请
        /// </summary>
        public NewsEnum NewsType { get; set; }

        /// <summary>
        /// 新闻文章内容
        /// </summary>
        public string NewsContent { get; set; }

        /// <summary>
        /// 入党介绍人
        /// </summary>
        public int ToUser { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public int IsPublic { get; set; }
    }
}