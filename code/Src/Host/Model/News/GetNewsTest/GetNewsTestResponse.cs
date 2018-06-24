﻿using System.Collections.Generic;

namespace Eagles.Application.Model.News.GetNewsTest
{
    /// <summary>
    /// 新闻试卷查询
    /// </summary>
    public class GetNewsTestResponse : ResponseBase
    {
        /// <summary>
        /// 试卷List
        /// </summary>
        public List<AppQuestion> TestList { get; set; }
    }
}