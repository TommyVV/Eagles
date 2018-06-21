using System;

namespace Eagles.DomainService.Model.Org
{
    /// <summary>
    /// TB_ORG_RELATIONSHIP
    /// </summary>
    public class TbOrgRelationship
    {
        public DateTime CreateTime { get; set; }
        public DateTime EditTime { get; set; }
        public int OperId { get; set; }
        public int OrgId { get; set; }
        public int SubOrgId { get; set; }
    }
}