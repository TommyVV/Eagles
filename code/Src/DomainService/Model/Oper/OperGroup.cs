using System;

namespace Eagles.DomainService.Model.Oper
{
    /// <summary>
    /// TB_OPER_GROUP
    /// </summary>
    public class OperGroup
    {
        public DateTime CreateTime { get; set; }
        public DateTime EditTime { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int OrgId { get; set; }
    }
}