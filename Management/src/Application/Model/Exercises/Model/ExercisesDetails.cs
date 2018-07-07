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
        /// html 描述
        /// </summary>
        public string HtmlDescription { get; set; }

        /// <summary>
        /// 题目列表
        /// </summary>
        public List<Subject> SubjectList { get; set; }

        /// <summary>
        /// 内容 json格式 图片 文字
        /// </summary>
        public string Content { get; set; }
    }
}
