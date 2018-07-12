﻿using System.Collections.Generic;
using Eagles.Application.Model.Common;

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
        public List<UserNews> NewsList { get; set; }
    }
}