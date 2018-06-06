using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    class TaskDetailQueryResponse : ResponseBase
    {
        public string TaskId { get; set; }
        
        public string TaskTitle { get; set; }

        /// <summary>
        /// 任务创建者
        /// </summary>
        public string TaskFounder { get; set; }

        public string TaskStartDate { get; set; }

        public string TaskEndDate { get; set; }

        public string TaskStatus { get; set; }

        public string TaskContext { get; set; }
        
        public string TaskImageUrl { get; set; }

        public string TaskAttachmentName { get; set; }

        public string TaskAttachmentDownloadUrl { get; set; }
    }
}
