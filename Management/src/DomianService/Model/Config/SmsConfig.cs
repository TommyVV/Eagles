using System;

namespace Eagles.DomainService.Model.Config
{
    /// <summary>
    /// TB_SMS_CONFIG
    /// </summary>
    public class SmsConfig
    {
        public string AppId { get; set; }
        public string AppKey { get; set; }
        public DateTime CreateTime { get; set; }
        public int MaxCount { get; set; }
        public int Priority { get; set; }
        public int SendCount { get; set; }
        public string ServiceUrl { get; set; }
        public string SginKey { get; set; }
        public int Status { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
    }
}