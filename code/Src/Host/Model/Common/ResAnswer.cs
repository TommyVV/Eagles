
namespace Eagles.Application.Model.Common
{
    /// <summary>
    /// 答案
    /// </summary>
    public class ResAnswer
    {
        /// <summary>
        /// 题目Id
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// 答案Id
        /// </summary>
        public int AnswerId { get; set; }

        /// <summary>
        /// 是否正确
        /// </summary>
        public int IsRight { get; set; }
    }
}