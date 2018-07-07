using System;

namespace Eagles.DomainService.Model.Authority
{
    /// <summary>
    /// TB_AUTHORITY
    /// </summary>
    public class TbAuthority
    {
        public DateTime CreateTime { get; set; }
        public DateTime EditTime { get; set; }
        public string FunCode { get; set; }
        public int GroupId { get; set; }
        public int OperId { get; set; }
        public int OrgId { get; set; }
    }
}