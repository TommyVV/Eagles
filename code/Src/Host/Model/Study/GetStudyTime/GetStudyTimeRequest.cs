using System;

namespace Eagles.Application.Model.Study.GetStudyTime
{
    /// <summary>
    /// 学习时间查询
    /// </summary>
    public class GetStudyTimeRequest :RequestBase
    {
        /// <summary>
        /// 新闻id
        /// </summary>
        public int NewsId { get; set; }

        /// <summary>
        /// 新闻栏目id
        /// </summary>
        public int ModuleId { get; set; }
    }
}