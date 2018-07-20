using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Publicity.Request
{
    public class GetPublicTaskDetailRequest:RequestBase
    {
        /// <summary>
        /// 任务id
        /// </summary>
        public int TaskId { get; set; }
    }
}
