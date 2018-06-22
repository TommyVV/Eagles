namespace Eagles.Application.Model.SMSOrg.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class RemoveSMSOrgRequest : RequestBase
    {
        /// <summary>
        ///  '短信提供方ID',
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// 机构id
        /// </summary>
        public string OrgId { get; set; }
    }
}
