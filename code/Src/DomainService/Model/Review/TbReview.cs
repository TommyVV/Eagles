using System;

namespace Eagles.DomainService.Model.Review
{
    /// <summary>
    /// TB_REVIEW
    /// </summary>
    public class TbReview
    {
        public int BranchId { get; set; }
        public DateTime CreateTime { get; set; }
        public int NewsId { get; set; }
        public string NewsType { get; set; }
        public int OperId { get; set; }
        public int OrgId { get; set; }
        public string Result { get; set; }
        public int ReviewId { get; set; }
        public int ReviewStatus { get; set; }
    }
}