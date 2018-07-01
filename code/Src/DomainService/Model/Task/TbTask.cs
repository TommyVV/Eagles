﻿using System;

namespace Eagles.DomainService.Model.Task
{
    /// <summary>
    /// TB_TASK
    /// </summary>
    public class TbTask
    {
        public string Attach1 { get; set; }
        public string Attach2 { get; set; }
        public string Attach3 { get; set; }
        public string Attach4 { get; set; }
        public string AttachType1 { get; set; }
        public string AttachType2 { get; set; }
        public string AttachType3 { get; set; }
        public string AttachType4 { get; set; }
        public DateTime BeginTime { get; set; }
        public int BranchId { get; set; }
        public string BranchReview { get; set; }
        public int CanComment { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 任务发起人
        /// </summary>
        public int FromUser { get; set; }
        /// <summary>
        /// 任务负责人
        /// </summary>
        public int UserId { get; set; }

        public int IsPublic { get; set; }
        public int OrgId { get; set; }
        public string OrgReview { get; set; }
        public int Status { get; set; }
        public string TaskContent { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
    }
}