﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.ActivityTask
{
    public class ActivityTaskInfo
    {

        /// <summary>
        /// 主键
        /// </summary>
        public string ActivityTaskId { get; set; }
        /// <summary>
        /// 新闻名字
        /// </summary>
        public string ActivityTaskName { get; set; }

        /// <summary>
        /// 活动任务类型
        /// </summary>
        public ActivityTaskType ActivityTaskType { get; set; }

        /// <summary>
        /// 作者id
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 新闻图片
        /// </summary>
        public string ActivityTaskImg { get; set; }

       
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public AuditStatus AuditStatus { get; set; }
    }
}
