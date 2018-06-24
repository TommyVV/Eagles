using System.Collections.Generic;

namespace Eagles.Application.Model.News.GetNewsTest
{
    /// <summary>
    /// 试卷
    /// </summary>
    public class AppQuestion
    {
        /// <summary>
        /// 问题id
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// 问题
        /// </summary>
        public string Question { get; set; }
        
        /// <summary>
        /// 答案类型;是否允许多选;0:否;1:是
        /// </summary>
        public int Multiple { get; set; }

        /// <summary>
        /// 多选数量
        /// </summary>
        public int MultipleCount { get; set; }

        /// <summary>
        /// 答案
        /// </summary>
        public List<AppAnswer> AnswerList { get; set; }

    }
}