using System;

namespace Eagles.DomainService.Model.Sms
{
    /// <summary>
    /// TB_SMS_SEND_LOG
    /// </summary>
    public class TbSmsSendLog
    {
        public DateTime CreateTime { get; set; }
        public int OrgId { get; set; }
        public string Phone { get; set; }
        public string RequestMsg { get; set; }
        public string ResponseMsg { get; set; }
        public string SmsContent { get; set; }
        public int Status { get; set; }
        public int TraceId { get; set; }
        public int VendorId { get; set; }
    }
}