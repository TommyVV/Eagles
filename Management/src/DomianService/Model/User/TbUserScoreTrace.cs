using System;

namespace Eagles.DomainService.Model.User
{
    /// <summary>
    /// 
    /// </summary>
    public class TbUserScoreTrace
    {
        public string Comment { get; set; }
        public DateTime CreateTime { get; set; }
        public int OrgId { get; set; }
        public int OriScore { get; set; }
        public string RewardsType { get; set; }
        public int Score { get; set; }
        public int TraceId { get; set; }
        public int UserId { get; set; }
    }
}