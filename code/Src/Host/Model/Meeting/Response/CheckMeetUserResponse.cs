using System.Collections.Generic;
using Eagles.Application.Model.Enums;
using Eagles.Application.Model.Meeting.Model;

namespace Eagles.Application.Model.Meeting.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckMeetUserInfoResponse:ResponseBase
    {

        /// <summary>
        /// 
        /// </summary>
        public List<MeetUserResult> list { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CheckResult Result { get; set; }
    }
}
