using System;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Exercises.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Exercises
    {

        /// <summary>
        /// 支部id
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// 组织id
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 试卷名
        /// </summary>
        public string ExercisesName { get; set; }

        /// <summary>
        /// 试卷id
        /// </summary>
        public int ExercisesId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ExercisesType { get; set; }

        ///// <summary>
        ///// 来源
        ///// </summary>
        //public string Source { get; set; }
        
        /// <summary>
        /// 是否有积分奖励
        /// </summary>
        public int IsScoreAward { get; set; }

        /// <summary>
        /// 及格奖励积分
        /// </summary>
        public int PassAwardScore { get; set; }

        /// <summary>
        /// 题目积分
        /// </summary>
        public int SubjectScore { get; set; }

        /// <summary>
        /// 及格分
        /// </summary>
        public int PassScore { get; set; }

        ///// <summary>
        ///// 随机题目数量
        ///// </summary>
        //public int RandomSubjectSum { get; set; }
        
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 分钟
        /// </summary>
        public int LimitedTime { get; set; }

        /// <summary>
        /// 是否限制时间
        /// </summary>
        public bool HasLimitedTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// html 描述
        /// </summary>
        public string HtmlDescription { get; set; }

        ///// <summary>
        ///// 内容 json格式 图片 文字
        ///// </summary>
        //public string Content { get; set; }
    }
}