namespace Eagles.Application.Model.SMS.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class RemoveSMSRequset : RequestBase
    {
        /// <summary>
        ///  '短信提供方ID',
        /// </summary>
        public int VendorId { get; set; }
    }
}
