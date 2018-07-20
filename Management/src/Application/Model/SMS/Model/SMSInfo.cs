using System;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.SMS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class SMSInfo
    {
        /// <summary>
        ///  '短信提供方ID',（新增时无需传递）
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// 短信提供方名称
        /// </summary>
        public string VendorName { get; set; }

        /// <summary>
        /// 已发送数量
        /// </summary>
        public int SendCount { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 短信方appId
        /// </summary>
        /// <returns></returns>
        public string AppId { get; set; }

        /// <summary>
        /// 短信方appKey
        /// </summary>
        /// <returns></returns>
        public string AppKey { get; set; }
        /// <summary>
        /// 签名key
        /// </summary>
        public string SginKey { get; set; }

        /// <summary>
        /// 接口地址
        /// </summary>
        /// <returns></returns>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// 短信总数
        /// </summary>
        /// <returns></returns>
        public int MaxCount { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 状态 0：正常；1：禁用
        /// </summary>
        public Status Status { get; set; }
    }
}
