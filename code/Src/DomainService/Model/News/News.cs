using System;

namespace Eagles.DomainService.Model.News
{
    /// <summary>
    /// TB_NEWS
    /// </summary>
    public class News
    {
        public string Attach1 { get; set; }
        public string Attach2 { get; set; }
        public string Attach3 { get; set; }
        public string Attach4 { get; set; }
        public string Attach5 { get; set; }
        public string Author { get; set; }
        public DateTime BeginTime { get; set; }
        public int CanStudy { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime EndTime { get; set; }
        public string HtmlContent { get; set; }
        public string ImageUrl { get; set; }
        public int IsAttach { get; set; }
        public int IsClass { get; set; }
        public int IsImage { get; set; }
        public int IsLearning { get; set; }
        public int IsText { get; set; }
        public int IsVideo { get; set; }
        public int Module { get; set; }
        public int NewsId { get; set; }
        public int OperId { get; set; }
        public int OrgId { get; set; }
        public int ReviewId { get; set; }
        public string ShortDesc { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
        public int TestId { get; set; }
        public string Title { get; set; }
        public int ViewCount { get; set; }
    }
}