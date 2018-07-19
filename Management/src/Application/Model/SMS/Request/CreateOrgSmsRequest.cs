namespace Eagles.Application.Model.SMS.Request
{
    public class CreateOrgSmsRequest
    {
        /// <summary>
        /// 机构id
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 选择配置的短信提供商id
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// 优先级，数字越大越靠前
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 最大可发送数量
        /// </summary>
        public int MaxSendCount { get; set; }

        /// <summary>
        /// 已发送数量，编辑时，不可更改此值
        /// </summary>
        public int UseCount { get; set; }

        /// <summary>
        /// 状态;0:正常:1:禁用
        /// </summary>
        public string Status { get; set; }
    }
}
