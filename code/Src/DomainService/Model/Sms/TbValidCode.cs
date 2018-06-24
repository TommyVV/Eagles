using System;

namespace Eagles.DomainService.Model.Sms
{
    public class TbValidCode
    {
        public DateTime CreateTime { get; set; }
        public DateTime ExpireTime { get; set; }
        public int OrgId { get; set; }
        public string Phone { get; set; }
        public int Seq { get; set; }
        public int TraceId { get; set; }
        public int Code { get; set; }
    }
}