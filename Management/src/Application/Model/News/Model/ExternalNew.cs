using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.News.Model
{
    public class ExternalNew
    {
        /// <summary>
        /// 外部链接url
        /// </summary>
        public string ExternalUrl { get; set; }

        /// <summary>
        /// 新闻名字
        /// </summary>
        public string NewsName { get; set; }

        /// <summary>
        /// 新闻图片
        /// </summary>
        public string NewsImg { get; set; }
    }
}
