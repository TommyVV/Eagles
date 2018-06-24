﻿using System;
using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Task.CreateTask
{
    /// <summary>
    /// 任务发布
    /// </summary>
    public class CreateTaskRequest : RequestBase
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 任务发起人
        /// </summary>
        public string TaskFromUser { get; set; }
        
        /// <summary>
        /// 任务负责人
        /// </summary>
        public int TaskToUserId { get; set; }

        /// <summary>
        /// 任务开始时间
        /// </summary>
        public DateTime TaskBeginDate { get; set; }

        /// <summary>
        /// 任务结束时间
        /// </summary>
        public DateTime TaskEndDate { get; set; }

        /// <summary>
        /// 任务内容
        /// </summary>
        public string TaskContent { get; set; }

        /// <summary>
        /// 是否评论
        /// </summary>
        public int CanComment { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public int IsPublic { get; set; }

        public List<Attachment> AttachList { get; set; }
    }
}