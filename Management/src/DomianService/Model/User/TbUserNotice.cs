using System;

namespace Eagles.DomainService.Model.User
{
    /// <summary>
    /// TB_USER_NOTICE
    /// </summary>
    public class TbUserNotice
    {
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public int FromUser { get; set; }
        public int IsRead { get; set; }
        public int NewsId { get; set; }
        public int NewsType { get; set; }
        public int OrgId { get; set; }
        public DateTime ReadTime { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
    }
}