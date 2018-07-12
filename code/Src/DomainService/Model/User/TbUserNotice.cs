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
        /// <summary>
        /// 发送人
        /// </summary>
        public int FromUser { get; set; }
        public int IsRead { get; set; }
        public int NewsId { get; set; }
        public int NewsType { get; set; }
        public int OrgId { get; set; }
        public DateTime ReadTime { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// 接收用户
        /// </summary>
        public int UserId { get; set; }

        public string TargetUrl { get; set; }
    }
}