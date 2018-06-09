using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Meeting
{
    public class GetMeetingInfoResponse:ResponseBase
    {
        public MeetingInfo info { get; set; }
    }
}
