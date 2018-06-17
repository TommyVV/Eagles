namespace Eagles.Application.Model.Organization.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetOrganizationDetailsRequset : RequestBase
    {
        /// <summary>
        /// 凭证
        /// </summary>
        public string OrgId { get; set; }
    }
}
