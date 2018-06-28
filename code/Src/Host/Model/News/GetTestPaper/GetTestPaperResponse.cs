using System.Collections.Generic;

namespace Eagles.Application.Model.News.GetTestPaper
{
    /// <summary>
    /// 新闻试卷查询
    /// </summary>
    public class GetTestPaperResponse : ResponseBase
    {
        /// <summary>
        /// 试卷List
        /// </summary>
        public List<AppQuestion> TestList { get; set; }
    }
}