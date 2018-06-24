using System;

namespace Eagles.DomainService.Model.Org
{
    /// <summary>
    /// TB_BRANCH
    /// </summary>
    public class Branch
    {
        public string BranchDesc { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public DateTime CreateTime { get; set; }
        public int OrgId { get; set; }
    }
}