namespace Eagles.Application.Model.Exercises.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Option
    {
        /// <summary>
        /// 选项id（新增时，无需传入）
        /// </summary>
        public int OptionId { get; set; }

        /// <summary>
        /// 选项名称
        /// </summary>
        public string OptionName { get; set; }

        /// <summary>
        /// 问题id
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// 是否正确答案，0：是，1：否；2：为投票时，无正确答案
        /// </summary>
        public int IsRight { get; set; }

        /// <summary>
        /// 允许用户自定义输入：0：否；1：是；
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
