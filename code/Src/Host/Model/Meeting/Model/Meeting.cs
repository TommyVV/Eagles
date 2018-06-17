using System;
using System.Collections.Generic;

namespace Eagles.Application.Model.Meeting.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Meeting
    {
        /// <summary>
        /// 会议id
        /// </summary>
        public string MeetingId { get; set; }

        /// <summary>
        /// 会议名称
        /// </summary>
        public string MeetingNmae { get; set; }

        /// <summary>
        /// 会议发起人
        /// </summary>
        public string MeetingInitiator { get; set; }

        /// <summary>
        /// 会议参与人
        /// </summary>
        public List<String> Participants { get; set; }

    }
}
