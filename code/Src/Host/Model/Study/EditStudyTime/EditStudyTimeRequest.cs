using System;

namespace Eagles.Application.Model.Study.EditStudyTime
{
    /// <summary>
    /// 学习时间记录
    /// </summary>
    public class EditStudyTimeRequest : RequestBase
    {
        /// <summary>
        /// 学习时间
        /// </summary>
        public int StudyId { get; set; }

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