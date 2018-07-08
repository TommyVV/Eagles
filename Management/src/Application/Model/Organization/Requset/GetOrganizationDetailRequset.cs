namespace Eagles.Application.Model.Organization.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetOrganizationDetailRequset : RequestBase
    {
        /// <summary>
        /// 凭证
        /// </summary>
        public int OrgId { get; set; }
    }
}
