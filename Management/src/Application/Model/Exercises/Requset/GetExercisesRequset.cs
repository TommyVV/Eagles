using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Exercises.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetExercisesRequset : OrgListRequestBase
    {
        /// <summary>
        /// 在线考试=5,新闻系统=10,投票=20
        /// </summary>
        public ExercisesType ExercisesType { get; set; }

        /// <summary>
        /// 试卷名
        /// </summary>
        public string ExercisesName { get; set; }

        /// <summary>
        /// 状态 0;正常;1:禁用
        /// </summary>
        public Status Status { get; set; }

    }
}
