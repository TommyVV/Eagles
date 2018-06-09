using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Meeting
{
    public class ImportMeetUserInfoRequset
    {
        /// <summary>
        /// 导入会议人员名单
        /// </summary>
        public List<MeetUserInfo> list { get; set; }

        /// <summary>
        /// 会议id
        /// </summary>
        public string MeetingId { get; set; }

        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }
    }
}
