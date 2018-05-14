using Eagles.Application.Model.PartyMission;
using Eagles.Interface.Core.PartyMission;
using System.Web.Http;

namespace Eagles.Host.Controllers
{
    public class PartyMissionController : ApiController
    {
        private IPartyMissionHandler pmHandler;

        public PartyMissionController(IPartyMissionHandler pmHandler)
        {
            this.pmHandler = pmHandler;
        }

        /// <summary>
        /// Demo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetPartyMissionById")]
        [HttpPost]
        public PartyMissionResponse Demo(PartyMissionRequest request)
        {
            return pmHandler.GetPartyMissionById(request);
        }
    }
}
