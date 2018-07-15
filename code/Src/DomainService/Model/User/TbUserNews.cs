using System;
using Eagles.Application.Model.Enums;

namespace Eagles.DomainService.Model.User
{
    /// <summary>
    /// TB_USER_NEWS
    /// </summary>
    public class TbUserNews
    {
        public int BranchId { get; set; }
        public string BranchReview { get; set; }
        public DateTime CreateTime { get; set; }
        public string HtmlContent { get; set; }
        public int NewsId { get; set; }
        public NewsEnum NewsType { get; set; }
        public int OrgId { get; set; }
        public string OrgReview { get; set; }
        public int ReviewId { get; set; }
        public int RewardsScore { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public int ViewsCount { get; set; }
        public int IsPublic { get; set; }
    }
}