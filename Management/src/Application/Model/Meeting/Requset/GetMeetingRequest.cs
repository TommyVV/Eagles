namespace Eagles.Application.Model.Meeting.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetMeetingRequest : OrgListRequestBase
    {
        /// <summary>
        /// 会议名称
        /// </summary>
        public string MeetingNmae { get; set; }
    }
}
