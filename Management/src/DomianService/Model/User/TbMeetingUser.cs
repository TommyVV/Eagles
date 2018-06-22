using System;

namespace Eagles.DomainService.Model.User
{
    /// <summary>
    /// TB_MEETING_USER
    /// </summary>
    public class TbMeetingUser
    {
        public int BranchId { get; set; }
        public int NewsId { get; set; }
        public int OrgId { get; set; }
        public int UserId { get; set; }
    }
}