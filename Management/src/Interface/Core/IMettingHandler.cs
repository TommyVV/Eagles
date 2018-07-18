using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Meeting.Requset;
using Eagles.Application.Model.Meeting.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface IMettingHandler : IInterfaceBase
    {
        ImportMeetingResponse ImportMeetUserInfoRequset(ImportMeetUserInfoRequset requset);
        GetMeetingResponse GetMettingUsers(GetMeetingRequest requset);
    }
}
