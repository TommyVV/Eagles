namespace Eagles.Application.Model.Exercises.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Option
    {
        /// <summary>
        /// 
        /// </summary>
        public int OptionId { get; set; }

        /// <summary>
        /// 选项
        /// </summary>
        public string OptionName { get; set; }

        /// <summary>
        /// 问题id
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// 是否正确答案
        /// </summary>
        public int IsRight { get; set; }

        /// <summary>
        /// 自定义
        /// </summary>
        public int AnswerType { get; set; }

        /// <summary>
        /// 是否有图片
        /// </summary>
        public bool IsImg { get; set; }

        /// <summary>
        /// 投票时图片
        /// </summary>
        public string Img { get; set; }
    }
}
