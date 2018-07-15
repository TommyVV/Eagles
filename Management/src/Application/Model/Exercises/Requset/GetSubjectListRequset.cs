using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Exercises.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetSubjectListRequset: ListRequestBase
    {
        /// <summary>
        /// 问题
        /// </summary>
        public string Question { get; set; }
    }
}
