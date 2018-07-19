namespace Eagles.Application.Model.Publicity.Request
{
    public class GetPublicActivityDetailRequest:RequestBase
    {
        /// <summary>
        /// 公开活动id
        /// </summary>
        public int ActivityId { get; set; }
    }
}
