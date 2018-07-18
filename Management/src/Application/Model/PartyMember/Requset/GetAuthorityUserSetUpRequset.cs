using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.PartyMember.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAuthorityUserSetUpRequset : PageRequestBase
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 是否关联
        /// </summary>
        public IsRelation IsRelation { get; set; }

        /// <summary>
        /// 支部编号
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// 支部名称
        /// </summary>
        public int BranchName { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public int OrgName { get; set; }

        /// <summary>
        /// 机构编号
        /// </summary>
        public int OrgId { get; set; }
        


    }
}
