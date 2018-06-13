using System;

namespace Eagles.DomainService.Model.News
{
    /// <summary>
    /// TB_SYSTEM_NEWS
    /// </summary>
    public class SystemNews
    {
        public string HtmlDesc { get; set; }
        public string NewsContent { get; set; }
        public int NewsId { get; set; }
        public string NewsName { get; set; }
        public string NewsType { get; set; }
        public string NoticeTime { get; set; }
        public int OperId { get; set; }
        public int RepeatTime { get; set; }
        public int Status { get; set; }
    }
}