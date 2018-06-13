using System;

namespace Eagles.DomainService.Model.User
{
    /// <summary>
    /// TB_USER_TOKEN
    /// </summary>
    public class UserToken
    {
        public int BranchId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ExpireTime { get; set; }
        public int OrgId { get; set; }
        public string Token { get; set; }
        public int TokenType { get; set; }
        public int UserId { get; set; }
    }
}