namespace Eagles.Application.Model.PartyMember.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetPartyMemberRequest : OrgListRequestBase
    {
        /// <summary>
        /// 支部id（可选)
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// 党员名称(可选）
        /// </summary>
        public string UserName { get; set; }

    }
}
