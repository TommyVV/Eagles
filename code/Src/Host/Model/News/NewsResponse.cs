using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.News
{
    public class NewsResponse:ResponseBase
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<NewsInfo> List { get; set; }
    }
}
