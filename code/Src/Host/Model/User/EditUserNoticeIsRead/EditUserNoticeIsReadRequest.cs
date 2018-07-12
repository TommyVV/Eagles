namespace Eagles.Application.Model.User.EditUserNoticeIsRead
{
    /// <summary>
    /// 更新用户通知为已读
    /// </summary>
    public class EditUserNoticeIsReadRequest : RequestBase
    {
        /// <summary>
        /// 新闻id
        /// </summary>
        public int NewsId { get; set; }
    }
}