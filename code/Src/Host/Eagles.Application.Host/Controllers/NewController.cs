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
    public class NewController : ApiController
    {
        private readonly INewsHandler _NewController;

        public NewController(INewsHandler testHandler)
        {
            this._NewController = testHandler;
        }



        /// <summary>
        /// 编辑  新闻
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/EditNews")]
        [HttpPost]
        public ResponseBase EditNews(EditNewRequset requset)
        {
            return _NewController.EditNews(requset);
        }

        /// <summary>
        ///  新闻 栏目
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/RemoveNews")]
        [HttpPost]
        public ResponseBase RemoveNews(RemoveNewRequset requset)
        {
            return _NewController.RemoveNews(requset);
        }

        /// <summary>
        ///  新闻 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/GetNewsDetail")]
        [HttpPost]
        public GetNewDetailResponse GetNewsDetail(GetNewDetailRequset requset)
        {
            return _NewController.GetNewsDetail(requset);
        }

        /// <summary>
        /// 新闻 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/GetNews")]
        [HttpPost]
        public GetNewResponse GetNews(GetNewRequset requset)
        {
            return _NewController.GetNews(requset);
        }
    }
}