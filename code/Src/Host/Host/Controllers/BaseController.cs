using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Eagles.Application.Host.Common;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RequestAuthorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BaseController : ApiController
    {


    }
}
