using System;

namespace Eagles.DomainService.Model.User
{
    /// <summary>
    /// TB_USER_RELATIONSHIP
    /// </summary>
    public class UserRelationship
    {
        public int OrgId { get; set; }
        public int SubUserId { get; set; }
        public int UserId { get; set; }
    }
}