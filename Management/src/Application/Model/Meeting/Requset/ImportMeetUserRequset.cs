using System.Collections.Generic;
using Eagles.Application.Model.Meeting.Model;

namespace Eagles.Application.Model.Meeting.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class ImportMeetUserInfoRequset:RequestBase
    {
        /// <summary>
        /// 导入会议人员名单
        /// </summary>
        public List<MeetUser> List { get; set; }

        /// <summary>
        /// 会议id
        /// </summary>
        public int MeetingId { get; set; }
    }
}
