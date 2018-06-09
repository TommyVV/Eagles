using System;
using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Curd.Task.CreateTask
{
    /// <summary>
    /// 任务发布
    /// </summary>
    public class CreateTaskRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 上下级人物类型
        /// </summary>
        public string TaskType { get; set; }
        
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 任务发起人
        /// </summary>
        public string TaskFromUser { get; set; }

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
        public string CanComment { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public string IsPublic { get; set; }

        public List<Attachment> AttachList { get; set; }
    }
}