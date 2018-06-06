using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    /// <summary>
    /// 任务评论查询
    /// </summary>
    class TaskCommentQueryRequest : RequestBase
    {
        public string Token { get; set; }
        public string TaskId { get; set; }
    }
}
