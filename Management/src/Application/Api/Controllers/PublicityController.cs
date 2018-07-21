using System.Web.Http;
using Eagles.Application.Host.Common;
using Eagles.Application.Model;
using Eagles.Application.Model.Publicity.Request;
using Eagles.Application.Model.Publicity.Response;
using Eagles.Interface.Core;

namespace Eagles.Application.Host.Controllers
{
    public class PublicityController : ApiController
    {

    

        private readonly IPublicityHandler Handler;

        public PublicityController(IPublicityHandler handler)
        {
            Handler = handler;
        }

        /// <summary>
        /// 查询申请公开文章
        /// </summary>
        public ResponseFormat<GetPublicAritcleResponse> GetPublicArticle(RequestBase requset)
        {
            //todo 

            return ApiActuator.Runing(requset, (requset1) => Handler.GetPublicArticle(requset));
           // return new GetPublicAritcleResponse();
        }

        /// <summary>
        /// 查询申请公开文章详情
        /// </summary>
        public ResponseFormat<GetAritcleDetailResponse> GetAritcleDetail(GetPublicArticleDetailRequest requset)
        {
            return ApiActuator.Runing(requset, (requset1) => Handler.GetAritcleDetail(requset));
            //todo
            //return new GetAritcleDetailResponse();
        }

        /// <summary>
        /// 查询申请公开任务
        /// </summary>
        public ResponseFormat<GetPublicTaskResponse> GetPublicTask(RequestBase requset)
        {
            return ApiActuator.Runing(requset, (requset1) => Handler.GetPublicTask(requset));
            //todo
            // return new GetPublicTaskResponse();
        }

        /// <summary>
        /// 查询公开任务详情
        /// </summary>
        public ResponseFormat<GetPublicTaskDetailResponse> GetPublicTaskDetail(GetPublicTaskDetailRequest requset)
        {
            return ApiActuator.Runing(requset, (requset1) => Handler.GetPublicTaskDetail(requset));
            //todo
            //return new GetPublicTaskDetailResponse();
        }

        /// <summary>
        /// 查询申请公开活动
        /// </summary>
        public ResponseFormat<GetPublicActivityResponse> GetPublicActivity(RequestBase requset)
        {
            return ApiActuator.Runing(requset, (requset1) => Handler.GetPublicActivity(requset));
            //todo 
            //return new GetPublicActivityResponse();
        }

        /// <summary>
        /// 查询申请公开活动详情
        /// </summary>
        public ResponseFormat<GetPublicActivityDetailResponse> GetPublicActivityDetail(GetPublicActivityDetailRequest requset)
        {
            return ApiActuator.Runing(requset, (requset1) => Handler.GetPublicActivityDetail(requset));
            //todo 
            // return new GetPublicActivityDetailResponse();
        }


    }
}