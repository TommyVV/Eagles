using System;
using System.Web.Http;
using Eagles.Application.Model.Curd;
using Eagles.Interface.Core.Curd;

namespace Eagles.Host.Controllers
{
    public class CurdController : ApiController
    {
        private readonly ICurdHandler CurdHandler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="curdHandler"></param>
        public CurdController(ICurdHandler curdHandler)
        {
            this.CurdHandler = curdHandler;
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/AfterInfo")]
        [HttpPost]
        public AfterInfoResponse AfterInfo(AfterInfoRequest request)
        {
            return CurdHandler.AfterInfo(request);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetInfo")]
        [HttpPost]
        public GetInfoResponse GetInfo(GetInfoRequest request)
        {
            return CurdHandler.GetInfo(request);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/RemoveInfo")]
        [HttpPost]
        public RemoveInfoResponse RemoveInfo(RemoveInfoRequest request)
        {
            return CurdHandler.RemoveInfo(request);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("api/GetInfos")]
        [HttpPost]
        public GetInfosRespone GetInfos()
        {
            return CurdHandler.GetInfos();
        }
    }
}