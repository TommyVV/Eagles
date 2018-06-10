using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Exercises
{
    public class Subject
    {

        public int SubjectId { get; set; }

        /// <summary>
        /// 题目名称
        /// </summary>
        public int SubjectName { get; set; }

        /// <summary>
        /// 题目类型
        /// </summary>
        public SubjectType SubjectType { get; set; }



    }
}
