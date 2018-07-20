using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Eagles.Application.Host.Common;
using Eagles.Application.Model.Meeting.Requset;
using Eagles.Application.Model.Meeting.Response;
using Eagles.Interface.Core;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class MettingController : ApiController
    {
        private readonly IMettingHandler _MettingHandler;

        public MettingController(IMettingHandler mettingHandler)
        {
            _MettingHandler = mettingHandler;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        public ResponseFormat<ImportMeetingResponse> ImportMeetingUser(ImportMeetUserInfoRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _MettingHandler.ImportMeetUserInfoRequset(requset));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseFormat<GetMeetingResponse> GetMettingUsers(GetMeetingRequest requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _MettingHandler.GetMettingUsers(requset));

        }
    }
}