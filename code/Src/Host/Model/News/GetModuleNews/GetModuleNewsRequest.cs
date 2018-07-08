namespace Eagles.Application.Model.News.GetModuleNews
{
    /// <summary>
    /// 新闻模块查询
    /// </summary>
    public class GetModuleNewsRequest : QueryRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        public int ModuleId { get; set; }

        /// <summary>
        /// 新闻数量
        /// </summary>
        public int NewsCount { get; set; }
    }
}