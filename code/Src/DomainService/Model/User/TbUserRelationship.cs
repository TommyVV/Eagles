
namespace Eagles.DomainService.Model.User
{
    /// <summary>
    /// TB_USER_RELATIONSHIP
    /// </summary>
    public class TbUserRelationship
    {
        /// <summary>
        /// 组织id
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 领导id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 下级id
        /// </summary>
        public int SubUserId { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string Name { get; set; }
    }
}