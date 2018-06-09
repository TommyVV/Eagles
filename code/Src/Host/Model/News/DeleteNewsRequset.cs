using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.News
{
    public class DeleteNewsRequset
    {
        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 新闻名字
        /// </summary>
        public string NewsId { get; set; }
    }
}
