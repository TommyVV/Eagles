using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.ActivityTask.Model
{
    public class PublicTaskDetail
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
        /// 发起人
        /// </summary>
        public string FromUser { get; set; }

        /// <summary>
        /// 任务内容
        /// </summary>
        public string TaskContent { get; set; }

        /// <summary>
        /// 计划
        /// </summary>
        public List<Step> Steps { get; set; }

        /// <summary>
        /// 申请时间（格式：yyyy-MM-dd HH：mm：ss
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 附件列表
        /// </summary>
        public List<Attachment> Attachments { get; set; }
    }

    public class Step
    {
        /// <summary>
        /// 计划id
        /// </summary>
        public int StepId { get; set; }

        /// <summary>
        /// 计划名称
        /// </summary>
        public string StepName { get; set; }

        /// <summary>
        /// 计划内容
        /// </summary>
        public string StepContent { get; set; }
    }
}
