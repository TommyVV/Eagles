using System.Collections.Generic;
using Eagles.Application.Model.Meeting.Model;

namespace Eagles.Application.Model.Meeting.Response
{
    public class ImportMeetingResponse:ResponseBase
    {
        public List<MeetUser> ImportUsersResult { get; set; }
    }
}
