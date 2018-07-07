using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.News.CompleteTest
{
    /// <summary>
    /// 试卷完成接口
    /// </summary>
    public class CompleteTestRequest :RequestBase
    {
        /// <summary>
        /// 试卷Id
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// 试卷用时
        /// </summary>
        public int UseTime { get; set; }

        /// <summary>
        /// 答案List
        /// </summary>
        public List<ReqAnswer> TestList { get; set; }
    }
}