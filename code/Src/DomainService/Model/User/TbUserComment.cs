using System;

namespace Eagles.DomainService.Model.User
{
    /// <summary>
    /// TB_USER_COMMENT
    /// </summary>
    public class TbUserComment
    {
        public string CommentType { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public int MessageId { get; set; }
        public int OrgId { get; set; }
        public int ReviewStatus { get; set; }
        public DateTime ReviewTime { get; set; }
        public int ReviewUser { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
    }
}