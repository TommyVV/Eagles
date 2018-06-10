using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Exercises
{
    public class ExercisesInfo
    {
        /// <summary>
        /// 试卷名
        /// </summary>
        public string ExercisesName { get; set; }

        /// <summary>
        /// 试卷id
        /// </summary>
        public string ExercisesId { get; set; }

        public ExercisesType ExercisesType { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 内容 json格式 图片 文字
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否有积分奖励
        /// </summary>
        public bool IsScoreAward { get; set; }

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

        /// <summary>
        /// 随机题目数量
        /// </summary>
        public int RandomSubjectSum { get; set; }

        /// <summary>
        /// 题目列表
        /// </summary>
        public List<Subject> SubjectList { get; set; }

    }
}
