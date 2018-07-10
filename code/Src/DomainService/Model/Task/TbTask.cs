using System;

namespace Eagles.DomainService.Model.Task
{
    /// <summary>
    /// TB_TASK
    /// </summary>
    public class TbTask
    {
        /// <summary>
        /// 机构id
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 支部id
        /// </summary>
        public int BranchId { get; set; }
        /// <summary>
        /// 任务id
        /// </summary>
        public int TaskId { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 任务内容
        /// </summary>
        public string TaskContent { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Attach1 { get; set; }
        public string Attach2 { get; set; }
        public string Attach3 { get; set; }
        public string Attach4 { get; set; }
        public string AttachName1 { get; set; }
        public string AttachName2 { get; set; }
        public string AttachName3 { get; set; }
        public string AttachName4 { get; set; }
        /// <summary>
        /// 发起时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否允许评论;0:允许;1:禁止
        /// </summary>
        public int CanComment { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 任务发起人
        /// </summary>
        public int FromUser { get; set; }

        public string FromUserName { get; set; }

        /// <summary>
        /// 任务负责人
        /// </summary>
        public int UserId { get; set; }

        public string UserName { get; set; }
        public int IsPublic { get; set; }
        public string OrgReview { get; set; }
        public string BranchReview { get; set; }
        /// <summary>
        /// 创建类型 0上级发布 1下级向上级申请  
        /// </summary>
        public int CreateType { get; set; }
    }
}