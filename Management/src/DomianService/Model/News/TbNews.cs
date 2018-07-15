using System;

namespace Eagles.DomainService.Model.News
{
    /// <summary>
    /// TB_NEWS
    /// </summary>
    public class TbNews
    {
        /// <summary>
        /// 附件1
        /// </summary>
        public string Attach1 { get; set; }
        /// <summary>
        /// 附件2
        /// </summary>
        public string Attach2 { get; set; }
        /// <summary>
        /// 附件3
        /// </summary>
        public string Attach3 { get; set; }
        /// <summary>
        /// 附件4
        /// </summary>
        public string Attach4 { get; set; }
        /// <summary>
        /// 附件5
        /// </summary>
       // public string Attach5 { get; set; }


        public string AttachName1 { get; set; }

        public string AttachName2 { get; set; }

        public string AttachName3 { get; set; }

        public string AttachName4 { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 是否允许学习
        /// </summary>
        public int CanStudy { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        public string HtmlContent { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 有附件
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
        /// 是学习心得
        /// </summary>
        public int IsLearning { get; set; }
        /// <summary>
        /// 是文章
        /// </summary>
        public int IsText { get; set; }
        /// <summary>
        /// 有视频
        /// </summary>
        public int IsVideo { get; set; }
        /// <summary>
        /// 所属栏目
        /// </summary>
        public int Module { get; set; }
        /// <summary>
        /// 新闻id
        /// </summary>
        public int NewsId { get; set; }
        /// <summary>
        /// 操作员id
        /// </summary>
        public int OperId { get; set; }
        /// <summary>
        /// 组织id
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 审核id
        /// </summary>
        public int ReviewId { get; set; }
        /// <summary>
        /// 简单描述
        /// </summary>
        public string ShortDesc { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 状态;
        /// 0:正常 
        /// -1:待审核;'
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 试卷id
        /// </summary>
        public int TestId { get; set; }
        /// <summary>
        /// 新闻名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 阅读数量
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 外部链接url
        /// </summary>
        public string ExternalUrl { get; set; }
        /// <summary>
        /// 是否有url
        /// </summary>
        public int IsExternal { get; set; }
        
        public int NewsType { get; set; }




    }
}