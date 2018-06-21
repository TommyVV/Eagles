using System;
using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.AppModel.News
{
    /// <summary>
    /// 新闻试卷查询
    /// </summary>
    public class GetNewsTestResponse : ResponseBase
    {
        /// <summary>
        /// 
        /// </summary>
        private List<NewsTest> NewsTestList { get; set; }
    }
}