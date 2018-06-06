using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    /// <summary>
    /// 文章列表查询
    /// </summary>
    class ArticleQueryRequest : RequestBase
    {
        public string Token { get; set; }

        public string ArticlType { get; set; }
    }
}
