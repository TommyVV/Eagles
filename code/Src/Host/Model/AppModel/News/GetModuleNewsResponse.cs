using System.Collections.Generic;

namespace Eagles.Application.Model.AppModel.News
{
    /// <summary>
    /// 新闻模块查询
    /// </summary>
    public class GetModuleNewsResponse:ResponseBase
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Common.News> NewsInfos { get; set; }
    }
}