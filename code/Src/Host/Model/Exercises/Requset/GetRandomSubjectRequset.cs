namespace Eagles.Application.Model.Exercises.Requset
{
    /// <summary>
    /// 生成习题
    /// </summary>
    public class GetRandomSubjectRequset:OrgListRequestBase
    {
        /// <summary>
        /// 随机题目数量
        /// </summary>
        public int RandomSubjectSum { get; set; }

        /// <summary>
        /// 试卷id
        /// </summary>
        public int ExercisesId { get; set; }
    }
}
