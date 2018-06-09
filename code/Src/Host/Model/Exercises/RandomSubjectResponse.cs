using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Exercises
{
    public class RandomSubjectResponse : ResponseBase
    { 
        
        /// <summary>
      /// 题目列表
      /// </summary>
        public List<Subject> SubjectList { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }

    }
}
