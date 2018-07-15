
namespace Eagles.Application.Model.TestPaper.GetTestPaper
{
    /// <summary>
    /// 答案
    /// </summary>
    public class AppAnswer
    {
        /// <summary>
        /// 问题编号
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// 答案类型;0:默认 1:自定义
        /// </summary>
        public int AnswerType { get; set; }

        /// <summary>
        /// 选项编号
        /// </summary>
        public int AnswerId { get; set; }

        /// <summary>
        /// 选项
        /// </summary>
        public string Answer { get; set; }
        
        /// <summary>
        /// 答案是否是正确答案;0;是;1:否
        /// </summary>
        public int IsRight { get; set; }

        /// <summary>
        /// 选项图片
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 答案选择人数
        /// </summary>
        public int UserCount { get; set; }
    }
}