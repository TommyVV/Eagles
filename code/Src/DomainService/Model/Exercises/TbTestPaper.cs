using System;
using Eagles.Application.Model.Enums;

namespace Eagles.DomainService.Model.Exercises
{
    /// <summary>
    /// TB_TEST_PAPER
    /// </summary>
    public class TbTestPaper
    {
        /// <summary>
        /// 支部id
        /// </summary>
        public int BranchId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 是否限制时间
        /// </summary>
        public int HasLimitedTime { get; set; }
        /// <summary>
        /// 是否奖励积分
        /// </summary>
        public int HasReward { get; set; }
        /// <summary>
        /// 时间(分钟)
        /// </summary>
        public int LimitedTime { get; set; }
        /// <summary>
        /// 操作员id
        /// </summary>
        public int OperId { get; set; }
        /// <summary>
        /// 组织id
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 及格分值
        /// </summary>
        public int PassScore { get; set; }
        /// <summary>
        /// 每一题分值
        /// </summary>
        public int QuestionSocre { get; set; }
        /// <summary>
        /// 状态;0;正常;1:禁用
        /// </summary>
        public Status Status { get; set; }
        /// <summary>
        /// 试卷id
        /// </summary>
        public int TestId { get; set; }
        /// <summary>
        /// 试卷名称
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// html描述
        /// </summary>
        public string HtmlDescription { get; set; }

        /// <summary>
        /// 试卷类型
        /// </summary>
        public ExercisesType TestType { get; set; }

        /// <summary>
        /// 及格奖励积分
        /// </summary>
        public int PassAwardScore { get; set; }
    }
}
