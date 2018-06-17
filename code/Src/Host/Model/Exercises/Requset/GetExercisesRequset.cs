using Eagles.Application.Model.enums;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Exercises.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetExercisesRequset : OrgListRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ExercisesType ExercisesType { get; set; }

        /// <summary>
        /// 试卷名
        /// </summary>
        public string ExercisesName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; }

    }
}
