﻿using System;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Audit.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Audit
    {
        /// <summary>
        /// 审核流水id 后台主键
        /// </summary>
        public int AuditId { get; set; }

        /// <summary>
        /// 审核名称
        /// </summary>
        public int AuditName { get; set; }

        /// <summary>
        /// 发起人名字
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }


        /// <summary>
        /// 审核状态
        /// </summary>
        public AuditStatus AuditStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 审核类型
        ///00:文章
        ///10:任务
        ///20:活动
        /// </summary>
        public string NewsType { get; set; }
    }
}
