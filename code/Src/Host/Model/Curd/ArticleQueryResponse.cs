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
    class ArticleQueryResponse :ResponseBase
    {
        public List<Article> ArticleList { get; }
    }

    class Article
    {
        string ArticleId { get; set; }

        string ArticleTitle { get; set; }

        string ArticlType { get; set; }

        string ArticlContent { get; set; }

        string ArticlDate { get; set; }

        string ArticlImg { get; set; }
    }
}
