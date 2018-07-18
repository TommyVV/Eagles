using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Eagles.Application.Model.Meeting.Requset;
using Eagles.Application.Model.Meeting.Response;

namespace Eagles.Application.Host.Controllers
{
    public class MettingController:ApiController
    {
        public MettingController()
        {

        }

        public ImportMeetingResponse ImportMeetingUser(ImportMeetUserInfoRequset requset)
        {
            //todo
            return new ImportMeetingResponse();
        }

        public GetMeetingResponse GetMettingUsers(GetMeetingRequest request)
        {
            //todo
            return new GetMeetingResponse();
        }
    }
}