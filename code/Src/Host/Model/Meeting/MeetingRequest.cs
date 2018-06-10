using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Meeting
{
    public class MeetingRequest:RequestBase
    {
        /// <summary>
        /// 机构号
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 会议名称
        /// </summary>
        public string MeetingNmae { get; set; }
    }
}
