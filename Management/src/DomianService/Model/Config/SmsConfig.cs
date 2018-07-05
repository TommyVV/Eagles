using System;
using Eagles.Application.Model.Enums;

namespace Eagles.DomainService.Model.Config
{
    /// <summary>
    /// TB_SMS_CONFIG
    /// </summary>
    public class SmsConfig
    {
        /// <summary>
        /// 短信方appId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 短信方appKey
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 最大发送数量
        /// </summary>
        public int MaxCount { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 已发送数量
        /// </summary>
        public int SendCount { get; set; }
        /// <summary>
        /// 接口地址
        /// </summary>
        public string ServiceUrl { get; set; }
        /// <summary>
        /// 签名key
        /// </summary>
        public string SginKey { get; set; }
        /// <summary>
        /// 状态;0:正常:1:禁用
        /// </summary>
        public Status Status { get; set; }
        /// <summary>
        /// 短信提供方ID
        /// </summary>
        public int VendorId { get; set; }
        /// <summary>
        /// 短信提供方名称
        /// </summary>
        public string VendorName { get; set; }
    }
}