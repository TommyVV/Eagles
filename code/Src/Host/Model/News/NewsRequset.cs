using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.News
{
    public class NewsRequset:RequestBase
    {
        /// <summary>
        /// 作者id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 新闻名字
        /// </summary>
        public string  NewsName { get; set; }


        static readonly DateTime Dt = DateTime.Now;

        /// <summary>
        /// 统计时间
        /// </summary>
        public DateTime StartTime { get; set; } = Dt;

        /// <summary>
        /// 统计结束时间
        /// </summary>
        public DateTime EndTime { get; set; } = Dt;

        /// <summary>
        /// 是否查询全部 为frue时 不带统计时间筛选条件
        /// </summary>
        public bool IsSearchDateTime { get; set; } = false;
    }
}
