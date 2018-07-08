using System.Web.Http;
using Eagles.Application.Model.Area;
using Eagles.Interface.Core.Area;

namespace Eagles.Application.Host.Controllers
{
    public class AreaController : ApiController
    {
        private readonly IAreaHandler areaHandler;

        public AreaController(IAreaHandler areaHandler)
        {
            this.areaHandler = areaHandler;
        }

        /// <summary>
        /// 获取省市区
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public AreaResponse GetAreaInfo()
        {
            return areaHandler.Process();
        }
    }
}
