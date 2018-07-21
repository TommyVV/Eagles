using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Publicity.Model
{
    public class PublicTask
    {
        /// <summary>
        /// 任务id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 任务标题
        /// </summary>
        public string TaskTitle { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string ResponsibleUserName { get; set; }

        /// <summary>
        /// 申请时间（格式：yyyy-MM-dd HH：mm：ss
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
