namespace Eagles.Application.Model.SMS.Request
{
    public class GetSMSDetailRequset : RequestBase
    {
        /// <summary>
        ///  '短信提供方ID',
        /// </summary>
        public int VendorId { get; set; }
    }
}
