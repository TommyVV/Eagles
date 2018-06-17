using System;
using System.Collections.Generic;

namespace Eagles.Application.Model.News.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class NewsDetail : News
    {
        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime EnableTime { get; set; }

        /// <summary>
        /// 内容 json格式 图片 文字
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 习题id
        /// </summary>
        public int ExampleId { get; set; }

        /// <summary>
        /// 栏目id
        /// </summary>
        public int ColumnId { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public List<string> Category { get; set; }

        /// <summary>
        /// 附件 json格式
        /// </summary>
        public string Enclosure { get; set; }
    }
}
