using System;
using System.Linq;
using System.Web.Http;
using Eagles.Application.Model.Curd.Activity.CreateActivity;
using Eagles.Interface.Core.Activity;

namespace Eagles.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ActivityController : ApiController
    {
        private IActivityHandler activityHandler;

        /// <summary>
        /// Demo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/CreateActivity")]
        [HttpPost]
        public CreateActivityResponse CreateActivity(CreateActivityRequest request)
        {
            return activityHandler.CreateActivity(request);
        }
    }
}