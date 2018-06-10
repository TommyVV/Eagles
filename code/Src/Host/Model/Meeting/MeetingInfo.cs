using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Meeting
{
    public class MeetingInfo
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
