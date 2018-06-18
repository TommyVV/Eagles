using System;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.News.Model
{
    /// <summary>
    /// 新闻信息
    /// </summary>
    public class New
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
        public string Author { get; set; }

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
        
        /// <summary>
        ///新闻类型 
        /// </summary>
        public NewsType NewsType { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public  string UserName { get; set; }
    }
}
