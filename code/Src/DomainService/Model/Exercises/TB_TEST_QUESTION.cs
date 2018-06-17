using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.DomainService.Model.Exercises
{
    public class TB_TEST_QUESTION
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
