using System;

namespace Eagles.DomainService.Model.Org
{
    /// <summary>
    /// TB_ORG_SMS_CONFIG
    /// </summary>
    public class OrgSmsConfig
    {
        public DateTime CreateTime { get; set; }
        public int MaxCount { get; set; }
        public int OperId { get; set; }
        public int OrgId { get; set; }
        public int Priority { get; set; }
        public int SendCount { get; set; }
        public int Status { get; set; }
        public int VendorId { get; set; }
    }
}