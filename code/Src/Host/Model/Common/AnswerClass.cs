
namespace Eagles.Application.Model.Common
{
    /// <summary>
    /// 答案
    /// </summary>
    public class AnswerClass
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int IsRight { get; set; }
    }
}