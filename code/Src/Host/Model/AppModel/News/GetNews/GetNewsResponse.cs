﻿using System;
using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.AppModel.News.GetNews
{
    /// <summary>
    /// 文章列表查询
    /// </summary>
    public class GetNewsResponse : ResponseBase
    {
        /// <summary>
        /// NewsList
        /// </summary>
        public List<Common.News> NewsList { get; set; }
    }
}