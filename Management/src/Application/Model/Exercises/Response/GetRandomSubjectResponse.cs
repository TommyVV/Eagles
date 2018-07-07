using System.Collections.Generic;
using Eagles.Application.Model.Exercises.Model;

namespace Eagles.Application.Model.Exercises.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetRandomSubjectResponse 
    { 
        
        /// <summary>
      /// 题目列表
      /// </summary>
        public List<Subject> SubjectList { get; set; }

    }
}
