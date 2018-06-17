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
        public string UserName { get; set; }

        /// <summary>
        /// 是否关联
        /// </summary>
        public bool IsRelation { get; set; }
    }
}
