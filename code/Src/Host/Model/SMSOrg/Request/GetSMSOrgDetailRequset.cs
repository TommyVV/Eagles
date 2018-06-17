namespace Eagles.Application.Model.SMSOrg.Request
{
    public class GetSMSOrgDetailRequset : RequestBase
    {
        /// <summary>
        ///  '短信机构提供方ID',
        /// </summary>
        public int VendorId { get; set; }

           /// <summary>
        /// 机构id
        /// </summary>
        public string OrgId { get; set; }
    }
}
