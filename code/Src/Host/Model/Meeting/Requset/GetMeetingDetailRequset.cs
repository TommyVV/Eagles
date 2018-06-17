namespace Eagles.Application.Model.Meeting.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetMeetingDetailRequset:RequestBase
    {
        /// <summary>
        /// 会议id
        /// </summary>
        public string MeetingId { get; set; }
    }
}
