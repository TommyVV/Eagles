using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.News.CompleteTest
{
    /// <summary>
    /// 试卷完成接口
    /// </summary>
    public class CompleteTestResponse : ResponseBase
    {
        /// <summary>
        /// 答题分数
        /// </summary>
        public int TestScore { get; set; }

        /// <summary>
        /// 答题积分
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 答题用时
        /// </summary>
        public int UseTime { get; set; }

        /// <summary>
        /// 答题List
        /// </summary>
        public List<ResAnswer> TestList { get; set; }
    }
}