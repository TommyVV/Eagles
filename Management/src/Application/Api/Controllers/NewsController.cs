using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.News.Requset;
using Eagles.Application.Model.News.Response;
using Eagles.Interface.Core;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class NewsController : ApiController
    {
        private readonly INewsHandler testHandler;

        public NewsController(INewsHandler testHandler)
        {
            this.testHandler = testHandler;
        }
        
        /// <summary>
        /// 编辑  新闻
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseBase EditNews(EditNewRequset requset)
        {
            return testHandler.EditNews(requset);
        }

        /// <summary>
        ///  新闻 栏目
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseBase RemoveNews(RemoveNewRequset requset)
        {
            return testHandler.RemoveNews(requset);
        }

        /// <summary>
        /// 新闻 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public GetNewResponse GetNews(GetNewRequset requset)
        {
            return testHandler.GetNews(requset);
        }

        /// <summary>
        ///  新闻 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public GetNewDetailResponse GetNewsDetail(GetNewDetailRequset requset)
        {
            return testHandler.GetNewsDetail(requset);
        }

    }
}