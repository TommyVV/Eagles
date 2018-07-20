namespace Eagles.Application.Model.Meeting.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetMeetingRequest : OrgListRequestBase
    {
        /// <summary>
        /// 会议名称(可选参数）
        /// </summary>
        public string MeetingNmae { get; set; }

        /// <summary>
        /// 会议id，查询会议参与用户详情时，传递
        /// </summary>
        public int MeetingId { get; set; }
    }
}
