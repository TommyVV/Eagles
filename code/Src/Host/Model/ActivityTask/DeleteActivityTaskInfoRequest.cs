using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.ActivityTask
{
    public class DeleteActivityTaskInfoRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string ActivityTaskId { get; set; }

        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }
    }
}
