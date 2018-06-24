using System;

namespace Eagles.Application.Model.News.GetNewsDetail
{
    /// <summary>
    /// 新闻详情查询
    /// </summary>
    public class GetNewsDetailResponse : ResponseBase
    {
        /// <summary>
        /// 新闻编号
        /// </summary>
        public int NewsId { get; set; }

        /// <summary>
        /// 新闻名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 新闻内容
        /// </summary>
        public string HtmlContent { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }

         /// <summary>
        /// 所属
        /// </summary>
        public int Module { get; set; }
        
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 试卷Id
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// 是否有附件
        /// </summary>
        public int IsAttach { get; set; }
        
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
        /// 阅读数量
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// 是否允许学习
        /// </summary>
        public int CanStudy { get; set; }
    }
}