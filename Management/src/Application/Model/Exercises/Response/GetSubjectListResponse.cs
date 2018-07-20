using System.Collections.Generic;
using Eagles.Application.Model.Exercises.Model;

namespace Eagles.Application.Model.Exercises.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetSubjectListResponse
    {
        /// <summary>
        /// 题目列表
        /// </summary>
        public List<Subject> List { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
