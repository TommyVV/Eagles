using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.PartyMember.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAuthorityUserSetUpRequset : RequestBase
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
        /// 机构编号
        /// </summary>
        public int OrgId { get; set; }
        


    }
}
