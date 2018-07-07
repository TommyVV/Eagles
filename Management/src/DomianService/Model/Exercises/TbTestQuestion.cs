using System;

namespace Eagles.DomainService.Model.Exercises
{
    public class TbTestQuestion
    {
        /// <summary>
        /// 机构id
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 试卷问题id
        /// </summary>
        public int QuestionId { get; set; }
        /// <summary>
        /// 试卷id
        /// </summary>
        public int TestId { get; set; }
    }
}