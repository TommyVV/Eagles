using System;
using System.Collections.Generic;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Activity.CreateActivity
{
    /// <summary>
    /// 活动发布
    /// </summary>
    public class CreateActivityRequest : RequestBase
    {
        /// <summary>
        /// 活动类型;0:报名;1:投票;2:问卷调查
        /// </summary>
        public ActivityTaskType ActivityType { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }

        /// <summary>
        /// 活动发起人
        /// </summary>
        public int ActivityFromUser { get; set; }

        /// <summary>
        /// 活动负责人
        /// </summary>
        public int ActivityToUserId { get; set; }

        /// <summary>
        /// 活动起始日期
        /// </summary>
        public DateTime ActivityBeginDate { get; set; }

        /// <summary>
        /// 活动结束日期
        /// </summary>
        public DateTime ActivityEndDate { get; set; }

        /// <summary>
        /// 活动内容
        /// </summary>
        public string ActivityContent { get; set; }

        /// <summary>
        /// 是否允许评论;0:允许;1:禁止
        /// </summary>
        public int CanComment { get; set; }

        /// <summary>
        /// 试卷编号
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public int IsPublic { get; set; }

        /// <summary>
        /// 附件列表
        /// </summary>
        public List<Attachment> AttachList { get; set; }
    }
}