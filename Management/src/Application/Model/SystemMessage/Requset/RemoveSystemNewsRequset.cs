namespace Eagles.Application.Model.SystemMessage.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class RemoveSystemNewsRequset : RequestBase
    {

        /// <summary>
        /// 系统消息id，后台生成
        /// </summary>
        public int NewsId { get; set; }
    }
}
