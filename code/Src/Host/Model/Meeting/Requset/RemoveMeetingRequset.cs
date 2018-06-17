namespace Eagles.Application.Model.Meeting.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class RemoveMeetingRequset:RequestBase
    {
        /// <summary>
        /// 会议id
        /// </summary>
        public string MeetingId { get; set; }
    }
}
