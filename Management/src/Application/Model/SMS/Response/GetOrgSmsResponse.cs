namespace Eagles.Application.Model.SMS.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetOrgSmsResponse
    {
        /// <summary>
        /// 机构id
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName { get; set; }

        /// <summary>
        /// 短信提供商
        /// </summary>
        public string SmsVendorName { get; set; }

        /// <summary>
        /// 最大发送数量
        /// </summary>
        public int MaxSendCount { get; set; }

        /// <summary>
        /// 已发送数量
        /// </summary>
        public int UseCount { get; set; }
    }
}
