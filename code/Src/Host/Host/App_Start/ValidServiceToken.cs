using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using Eagles.Base.Logger;
using Eagles.Base.Logger.Implement;
using Newtonsoft.Json;

namespace Eagles.Application.Host
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidServiceToken : AuthorizeAttribute
    {
        /// <summary>
        /// 授权接口
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            try
            {
                var serviceUrl = "http://51service.xyz/tokenService/api/token";
                var client = new HttpClient();
                var response = client.GetAsync(serviceUrl);
                var result = response.Result.Content.ReadAsStringAsync().Result;
                var resultObj = JsonConvert.DeserializeObject<Dictionary<string, bool>>(result);
                resultObj.TryGetValue("Token", out var token);
                if (!token)
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            return base.IsAuthorized(actionContext);
        }


    }
}