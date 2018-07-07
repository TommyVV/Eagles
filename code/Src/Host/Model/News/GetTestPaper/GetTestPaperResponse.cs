using System.Collections.Generic;

namespace Eagles.Application.Model.News.GetTestPaper
{
    /// <summary>
    /// 试卷查询
    /// </summary>
    public class GetTestPaperResponse
    {
        /// <summary>
        /// 满分
        /// </summary>
        public int FullScore { get; set; }

        /// <summary>
        /// 及格分
        /// </summary>
        public int PassScore { get; set; }

        /// <summary>
        /// 奖励积分
        /// </summary>
        public int PassAwardScore { get; set; }

        /// <summary>
        /// 时间(分钟)
        /// </summary>
        public int LimitedTime { get; set; }

        /// <summary>
        /// 题目List
        /// </summary>
        public List<AppQuestion> TestList { get; set; }
    }
}