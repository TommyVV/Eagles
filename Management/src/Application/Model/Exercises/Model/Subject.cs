using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Exercises.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Subject
    {


        ///// <summary>
        ///// 试卷id
        ///// </summary>
        //public int ExercisesId { get; set; }



        ///// <summary>
        ///// 题目类型
        ///// </summary>
        //public SubjectType SubjectType { get; set; }

        /// <summary>
        /// 问题
        /// </summary>
        public string Question { get; set; }
        /// <summary>
        /// 问题id
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// 答案类型;0:默认:1:自定义
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
        /// <summary>
        /// 机构ID
        /// </summary>
        public int OrgId { get; set; }

    }
}
