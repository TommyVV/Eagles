using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        [HttpGet]
        public AreaResponse GetAreaInfo()
        {
            return areaHandler.Process();
        }
    }
}
