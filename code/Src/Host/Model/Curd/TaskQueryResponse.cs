using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    /// <summary>
    /// 任务查询
    /// </summary>
    class TaskQueryResponse : ResponseBase
    {
        public List<Task> TaskList { get; }
    }
    
    class Task
    {
        string TaskId { get; set; }

        string TaskeTitle { get; set; }

        string TaskType { get; set; }
        
        string TaskImgUrl { get; set; }
    }
}
