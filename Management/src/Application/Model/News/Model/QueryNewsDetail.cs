using System;
using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.News.Model
{
    public class QueryNewsDetail : New
    {
        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime StarTime { get; set; }

        /// <summary>
        /// 内容 json格式 图片 文字
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 习题id
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// 栏目id
        /// </summary>
        public int ModuleId { get; set; }

        ///// <summary>
        ///// 分类
        ///// </summary>
        //public List<string> Category { get; set; }

        /// <summary>
        /// 附件 json格式
        /// </summary>
        //   public string Attach { get; set; }

        public List<Attachment> Attachments { get; set; }
        //
        // public List<Attachment> Attach { get; set; }
        /// <summary>
        /// 外部链接url
        /// </summary>
        public string ExternalUrl { get; set; }
        /// <summary>
        /// 是否有url
        /// </summary>
        public int IsExternal { get; set; }

        /// <summary>
        /// 新闻类型：0：新闻；1：会议
        /// </summary>
        public int NewsType { get; set; }

        /// <summary>
        /// 是否能学习
        /// </summary>
        public int CanStudy { get; set; }

        /// <summary>
        /// 是否有附件
        /// </summary>
        public int IsAttach { get; set; }

        /// <summary>
        /// 有课件
        /// </summary>
        public int IsClass { get; set; }
        /// <summary>
        /// 有图片
        /// </summary>
        public int IsImage { get; set; }

        /// <summary>
        /// 是学习心得（暂时不需要）
        /// </summary>
        public int IsLearning { get; set; }

        /// <summary>
        /// 有视频
        /// </summary>
        public int IsVideo { get; set; }

        /// <summary>
        /// 简单描述
        /// </summary>
        public string ShortDesc { get; set; }
    }
}
