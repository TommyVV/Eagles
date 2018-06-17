﻿using System;
using Eagles.Application.Model.enums;

namespace Eagles.Application.Model.SMSOrg.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class SMSOrg
    {
        /// <summary>
        ///  '短信提供方ID',
        /// </summary>
        public int VendorId { get; set; }
        /// <summary>
        /// 机构id
        /// </summary>
        public string OrgId { get; set; }
        /// <summary>
        /// 已发送数量
        /// </summary>
        /// <returns></returns>
        public int SendCount { get; set; }
        /// <summary>
        ///  最大发送数量
        /// </summary>
        /// <returns></returns>
        public int MaxCount { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        /// <returns></returns>
        public int OperId { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; }
    }
}