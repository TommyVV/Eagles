using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Exercises.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ExercisesDetails : Exercises
    {

    

        /// <summary>
        /// 题目列表
        /// </summary>
        public List<Subject> SubjectList { get; set; }

    }
}
