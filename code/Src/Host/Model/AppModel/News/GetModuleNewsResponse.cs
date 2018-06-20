using System.Collections.Generic;
using Eagles.Application.Model.News.Model;

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
        public List<New> NewsInfos { get; set; }
    }
}