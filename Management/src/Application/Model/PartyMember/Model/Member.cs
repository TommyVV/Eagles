namespace Eagles.Application.Model.PartyMember.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Member
    {
        /// <summary>
        /// 新增无需传入，修改时传入
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 党员姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 所属支部id
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// 所属支部名称
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// 登录手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 党员类型 0:党员; 1:预备党员
        /// </summary>
        public int MemberType { get; set; }

        /// <summary>
        /// 机构名
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 机构名
        /// </summary>
        public string OrgName { get; set; }
    }
}
