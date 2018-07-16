using System;
using System.Collections.Generic;
using Eagles.Application.Model.Common;

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

        ///// <summary>
        ///// 分类
        ///// </summary>
        //public List<string> Category { get; set; }

        /// <summary>
        /// 附件 json格式
        /// </summary>
        //   public string Attach { get; set; }

        public string Attach1 { get; set; }

        public string Attach2 { get; set; }
        public string Attach3 { get; set; }
        public string Attach4 { get; set; }
        // public string Attach5 { get; set; }


        public string AttachName1 { get; set; }

        public string AttachName2 { get; set; }

        public string AttachName3 { get; set; }

        public string AttachName4 { get; set; }
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

        public int NewsType { get; set; }

        public int CanStudy { get; set; }

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
        /// 是学习心得
        /// </summary>
        public int IsLearning { get; set; }

        public int IsVideo { get; set; }

        public string ShortDesc { get; set; }










    }


}
