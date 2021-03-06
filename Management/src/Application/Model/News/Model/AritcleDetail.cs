﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.News.Model
{
    public class AritcleDetail
    {
        /// <summary>
        /// 文章id
        /// </summary>
        public int NewsId { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        public string NewsTitle { get; set; }

        /// <summary>
        /// 文章作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 文章发布时间（时间格式:yyyy-MM-dd
        /// </summary>
        public DateTime  CreateTime { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string NewsDetail { get; set; }
    }
}
