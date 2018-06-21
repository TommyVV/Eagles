using System.Collections.Generic;

namespace Eagles.Application.Model.AppModel.News.GetNewsTest
{
    /// <summary>
    /// 新闻试卷查询
    /// </summary>
    public class GetNewsTestResponse : ResponseBase
    {
        /// <summary>
        /// 
        /// </summary>
        private List<AppQuestion> TestList { get; set; }
    }
}