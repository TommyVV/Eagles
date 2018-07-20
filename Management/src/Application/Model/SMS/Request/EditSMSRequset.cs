using Eagles.Application.Model.SMS.Model;

namespace Eagles.Application.Model.SMS.Request
{
    public class EditSMSRequset : RequestBase
    {
        /// <summary>
        /// 短信
        /// </summary>
        public SMSInfo SmsInfo { get; set; }
    }
}
