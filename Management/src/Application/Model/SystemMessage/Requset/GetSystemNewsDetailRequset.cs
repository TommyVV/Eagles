namespace Eagles.Application.Model.SystemMessage.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetSystemNewsDetailRequset:RequestBase
    {

        /// <summary>
        /// 系统消息id
        /// </summary>
        public int NewsId { get; set; }
    }
}
