namespace Eagles.Application.Model.Exercises.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Subject
    {

        /// <summary>
        /// 问题名称
        /// </summary>
        public string Question { get; set; }
        /// <summary>
        /// 问题id
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// 答案类型;0:默认:1:自定义：2：投票（无正确答案）
        /// </summary>
        public int Answer { get; set; }

        /// <summary>
        /// 是否允许多选;0:否;1:是
        /// </summary>
        public int Multiple { get; set; }

        /// <summary>
        /// 多选数量
        /// </summary>
        public int MultipleCount { get; set; }

    }
}
