using System.Collections.Generic;

namespace Eagles.DomainService.Model.App
{
    /// <summary>
    /// TB_APP_MODULE
    /// </summary>
    public class AppModule
    {
        public string ImageUrl { get; set; }
        public int IndexDisplay { get; set; }
        public int IndexPageCount { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int ModuleType { get; set; }
        public int OrgId { get; set; }
        public int Priority { get; set; }
        public string SmallImageUrl { get; set; }
        public string TragetUrl { get; set; }

        public List<News.News> News { get; set; }
    }
}