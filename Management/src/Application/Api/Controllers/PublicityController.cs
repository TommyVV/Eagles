using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.Publicity.Request;
using Eagles.Application.Model.Publicity.Response;

namespace Eagles.Application.Host.Controllers
{
    public class PublicityController : ApiController
    {
        /// <summary>
        /// 查询申请公开文章
        /// </summary>
        public GetPublicAritcleResponse GetPublicArticle(RequestBase request)
        {
            //todo 
            return new GetPublicAritcleResponse();
        }

        /// <summary>
        /// 查询申请公开文章详情
        /// </summary>
        public GetAritcleDetailResponse GetAritcleDetail(GetPublicArticleDetailRequest request)
        {
            //todo
            return new GetAritcleDetailResponse();
        }

        /// <summary>
        /// 查询申请公开任务
        /// </summary>
        public GetPublicTaskResponse GetPublicTask(RequestBase request)
        {
            //todo
            return new GetPublicTaskResponse();
        }

        /// <summary>
        /// 查询公开任务详情
        /// </summary>
        public GetPublicTaskDetailResponse GetPublicTaskDetail(GetPublicTaskDetailRequest request)
        {
            //todo
            return new GetPublicTaskDetailResponse();
        }

        /// <summary>
        /// 查询申请公开活动
        /// </summary>
        public GetPublicActivityResponse GetPublicActivity(RequestBase request)
        {
            //todo 
            return new GetPublicActivityResponse();
        }

        /// <summary>
        /// 查询申请公开活动详情
        /// </summary>
        public GetPublicActivityDetailResponse GetPublicActivityDetail(GetPublicActivityDetailRequest request)
        {
            //todo 
            return new GetPublicActivityDetailResponse();
        }


    }
}