namespace Eagles.Application.Model.Exercises.Requset
{
    /// <summary>
    /// 生成习题
    /// </summary>
    public class GetRandomSubjectRequset: RequestBase
    {
        /// <summary>
        /// 随机生成题目数量
        /// </summary>
        public int RandomSubjectSum { get; set; }

        /// <summary>
        /// 试卷id
        /// </summary>
        public int TestId { get; set; }
    }
}
