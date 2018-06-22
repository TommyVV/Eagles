namespace Eagles.Application.Model.SMSOrg.Response
{
   public  class GetSMSOrgDetailResponse : ResponseBase
    {
        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public SMSOrg.Model.SMSOrg Info { get; set; }
    }
}
