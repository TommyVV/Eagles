using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Meeting
{
    public class CheckMeetUserInfoResponse:ResponseBase
    {

        public List<MeetUserInfoResult> list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CheckResult Result { get; set; }
    }
}
