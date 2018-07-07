using System;
using System.Collections.Generic;

namespace Eagles.Application.Model.News.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class NewDetail : New
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

        /// <summary>
        /// 分类
        /// </summary>
        public List<string> Category { get; set; }

        /// <summary>
        /// 附件 json格式
        /// </summary>
        //   public string Attach { get; set; }

        public string Attach1 { get; set; }

        public string Attach2 { get; set; }
        public string Attach3 { get; set; }
        public string Attach4 { get; set; }
        public string Attach5 { get; set; }

        /// <summary>
        /// 外部链接url
        /// </summary>
        public string ExternalUrl { get; set; }
        /// <summary>
        /// 是否有url
        /// </summary>
        public int IsExternal { get; set; }
    }
}
