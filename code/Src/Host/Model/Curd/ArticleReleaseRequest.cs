using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    /// <summary>
    /// 文章发布
    /// </summary>
    class ArticleReleaseRequest :RequestBase
    {
        public string Token { get; set; }

        public string ArticlTitle { get; set; }

        public string ArticlType { get; set; }

        public string ArticlContent { get; set; }
    }
}
