using System;

namespace Eagles.Application.Model.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public enum ActivityType
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,
        /// <summary>
        /// 报名
        /// </summary>
        SignUp = 1,
        /// <summary>
        /// 投票
        /// </summary>
        Vote = 2,
        /// <summary>
        /// 问卷调查
        /// </summary>
        Questionnaire = 3
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ActivityPage
    {
        /// <summary>
        /// 所有活动
        /// </summary>
        All = 0,
        /// <summary>
        /// 我参加的
        /// </summary>
        Mine = 1,
        /// <summary>
        /// 待我参加的
        /// </summary>
        Other = 2
    }
}