using System;
using System.IO;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Eagles.Base.Json;
using Eagles.Base.Json.Implement;
using Eagles.Base.Logger;
using Eagles.Base.Logger.Implement;

namespace Eagles.Application.Host
{
    public class LogAttribute: ActionFilterAttribute
    {
        private readonly ILogger logger=new Logger();

        private readonly IJsonSerialize jsonSerialize=new JsonSerialize();

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                logger.LoggerInfo(jsonSerialize.SerializeObject(actionContext.ActionArguments));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            try
            {
                var response = GetResponseValues(actionExecutedContext);
                logger.LoggerInfo(response);
            }
            catch (Exception e)
            {
                
            }
            
            base.OnActionExecuted(actionExecutedContext);
        }


        public string GetRequestValues(HttpActionExecutedContext actionExecutedContext)
        {

            Stream stream = actionExecutedContext.Request.Content.ReadAsStreamAsync().Result;
            Encoding encoding = Encoding.UTF8;
            /*
                这个StreamReader不能关闭，也不能dispose， 关了就傻逼了
                因为你关掉后，后面的管道  或拦截器就没办法读取了
            */
            var reader = new StreamReader(stream, encoding);
            string result = reader.ReadToEnd();
            /*
            这里也要注意：   stream.Position = 0;
            当你读取完之后必须把stream的位置设为开始
            因为request和response读取完以后Position到最后一个位置，交给下一个方法处理的时候就会读不到内容了。
            */
            stream.Position = 0;
            return result;
        }

        /// <summary>
        /// 读取action返回的result
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        /// <returns></returns>
        public string GetResponseValues(HttpActionExecutedContext actionExecutedContext)
        {
            Stream stream = actionExecutedContext.Response.Content.ReadAsStreamAsync().Result;
            Encoding encoding = Encoding.UTF8;
            /*
            这个StreamReader不能关闭，也不能dispose， 关了就傻逼了
            因为你关掉后，后面的管道  或拦截器就没办法读取了
            */
            var reader = new StreamReader(stream, encoding);
            string result = reader.ReadToEnd();
            /*
            这里也要注意：   stream.Position = 0; 
            当你读取完之后必须把stream的位置设为开始
            因为request和response读取完以后Position到最后一个位置，交给下一个方法处理的时候就会读不到内容了。
            */
            stream.Position = 0;
            return result;
        }
    }
}