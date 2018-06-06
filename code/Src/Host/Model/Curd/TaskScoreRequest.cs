using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    /// <summary>
    /// 任务评分
    /// </summary>
    class TaskScoreRequest : RequestBase
    {
        public string Token { get; set; }
        public string TaskId { get; set; }
        public string Score { get; set; }
        public string ScoreUser { get; set; }
    }
}
