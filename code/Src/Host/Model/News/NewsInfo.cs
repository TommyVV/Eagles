using Eagles.Application.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.News
{
    /// <summary>
    /// 新闻信息
    /// </summary>
    public class NewsInfo
    {
        /// <summary>
        /// 新闻名字
        /// </summary>
        public int NewsId { get; set; }

        /// <summary>
        /// 新闻名字
        /// </summary>
        public string NewsName { get; set; }

        /// <summary>
        /// 作者id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 作者id
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 新闻图片
        /// </summary>
        public string NewsImg { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// 审核状态
        /// </summary>
        public AuditStatus AuditStatus { get; set; }
    }
}
