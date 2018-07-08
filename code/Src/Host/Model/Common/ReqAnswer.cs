
using System.Collections.Generic;

namespace Eagles.Application.Model.Common
{
    /// <summary>
    /// 答案
    /// </summary>
    public class ReqAnswer
    {
        /// <summary>
        /// 题目Id
        /// </summary>
        public int QuestionId { get; set; }

        
        public List<Answer> Answers { get; set; }
    }

    public class Answer
    {
        /// <summary>
        /// 答案Id
        /// </summary>
        public int AnswerId { get; set; }

        /// <summary>
        /// 自定义答案
        /// </summary>
        public string CustomizeAnswer { get; set; }
    }
}