namespace Eagles.Application.Model.AppModel.News.GetModuleNews
{
    /// <summary>
    /// 新闻模块查询
    /// </summary>
    public class GetModuleNewsRequest : RequestBase
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