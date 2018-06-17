namespace Eagles.Application.Model.AuthorityGroup.Requset
{
    /// <summary>
    /// 权限组列表请求
    /// </summary>
    public class GetAuthorityGroupRequset : OrgListRequestBase
    {
        /// <summary>
        /// 权限组名称
        /// </summary>
        public string AuthorityName { get; set; }
    }
}
