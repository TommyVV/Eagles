﻿using System;
using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Curd.Activity.CreateActivity
{
    /// <summary>
    /// 活动发布
    /// </summary>
    public class CreateActivityRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        //public string OrgId { get; set; }

        //public string BranchId { get; set; }

        /// <summary>
        /// 活动类型;0:报名;1:投票;2:问卷调查
        /// </summary>
        public string ActivityType { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }

        /// <summary>
        /// 活动发起人
        /// </summary>
        public string ActivityFromUser { get; set; }

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
        public string CanComment { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public string IsPublic { get; set; }

        /// <summary>
        /// 附件列表
        /// </summary>
        public List<Attachment> AttachList { get; set; }
    }
}