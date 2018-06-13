using System;

namespace Eagles.DomainService.Model.Org
{
    /// <summary>
    /// TB_ORG_INFO
    /// </summary>
    public class OrgInfo
    {
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime CreateTime { get; set; }
        public string District { get; set; }
        public DateTime EditTime { get; set; }
        public string Logo { get; set; }
        public int OperId { get; set; }
        public int OrgId { get; set; }
        public int OrgName { get; set; }
        public string Province { get; set; }
    }
}