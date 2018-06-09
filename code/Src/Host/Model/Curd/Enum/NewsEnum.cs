using System;

namespace Eagles.Application.Model.Curd.Enum
{
    /// <summary>
    /// 文章类型 00:文章 01:心得体会 02:会议
    /// </summary>
    public enum NewsEnum
    {
        /// <summary>
        /// 报名
        /// </summary>
        Article = 00,
        /// <summary>
        /// 投票
        /// </summary>
        Experience = 01,
        /// <summary>
        /// 问卷调查
        /// </summary>
        Meeting = 02
    }
}