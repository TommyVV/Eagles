namespace Eagles.Application.Model.UserMessage
{
    public class GetUserUnreadMessage:ResponseBase
    {
        /// <summary>
        /// 未读消息数量
        /// </summary>
        public int UnreadMessageCount { get; set; }
    }
}
