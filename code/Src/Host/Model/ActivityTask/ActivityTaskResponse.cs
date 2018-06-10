using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.ActivityTask
{
    public class ActivityTaskResponse : ResponseBase
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<ActivityTaskInfo> List { get; set; }
    }
}
