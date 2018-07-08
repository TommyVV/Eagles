using System;
using Eagles.Application.Model.Enums;

namespace Eagles.DomainService.Model.Activity
{
    /// <summary>
    /// TB_ACTIVITY
    /// </summary>
    public class TbActivity
    {
        /// <summary>
        /// 活动id
        /// </summary>
        public int ActivityId { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }
        /// <summary>
        /// 活动类型;0:报名;1:投票;2:问卷调查
        /// </summary>
        public ActivityType ActivityType { get; set; }
        /// <summary>
        /// 附件1
        /// </summary>
        public string Attach1 { get; set; }
        /// <summary>
        /// 附件2
        /// </summary>
        public string Attach2 { get; set; }
        /// <summary>
        /// 附件3
        /// </summary>
        public string Attach3 { get; set; }
        /// <summary>
        /// 附件4
        /// </summary>
        public string Attach4 { get; set; }
        /// <summary>
        /// 附件1类型;0:图片;1:其他
        /// </summary>
        public string AttachName1 { get; set; }
        /// <summary>
        /// 附件1类型;0:图片;1:其他
        /// </summary>
        public string AttachName2 { get; set; }
        /// <summary>
        /// 附件1类型;0:图片;1:其他
        /// </summary>
        public string AttachName3 { get; set; }
        /// <summary>
        /// 附件1类型;0:图片;1:其他
        /// </summary>
        public string AttachName4 { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 支部id
        /// </summary>
        public int BranchId { get; set; }
        /// <summary>
        /// 支部审核状态
        ///0:审核通过;
        ///-1:待审核
        ///1:审核不通过
        /// </summary>
        public string BranchReview { get; set; }
        /// <summary>
        /// 是否允许评论;0:允许;1:禁止
        /// </summary>
        public int CanComment { get; set; }
        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 发起人
        /// </summary>
        public int FromUser { get; set; }
        /// <summary>
        /// 活动内容
        /// </summary>
        public string HtmlContent { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 是否公开
        /// </summary>
        public int IsPublic { get; set; }
        /// <summary>
        /// 每人最大参与次数
        /// </summary>
        public int MaxCount { get; set; }
        /// <summary>
        /// 最大参与人数
        /// </summary>
        public int MaxUser { get; set; }
        /// <summary>
        /// 机构id
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 机构审核状态;
        ///0:审核通过;
        ///-1:待审核
        ///1:审核不通过
        /// </summary>
        public string OrgReview { get; set; }
        /// <summary>
        /// 状态;
        ///-1:下级发给上级审核任务是否允许开始
        ///0:初始状态;(上级发给下级的初始状态) 
        ///1:活动申请完成
        ///2.活动完成
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 问卷调查试卷id
        /// </summary>
        public int TestId { get; set; }
        /// <summary>
        /// 负责人Id
        /// </summary>
        public int ToUserId { get; set; }

        /// <summary>
        /// 创建类型 0上级发布 1下级向上级申请  
        /// </summary>
        public int CreateType { get; set; }
    }
}