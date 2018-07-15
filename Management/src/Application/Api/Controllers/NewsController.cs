using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.News.Requset;
using Eagles.Application.Model.News.Response;
using Eagles.Base;
using Eagles.Interface.Core;

using Eagles.Application.Host.Common;
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
        public ResponseFormat<bool> EditNews(EditNewRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => testHandler.EditNews(requset));
        }

        /// <summary>
        /// 导入 新闻
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> ImportNews(ImportNewRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => testHandler.ImportNews(requset));
        }

        /// <summary>
        ///  新闻 栏目
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> RemoveNews(RemoveNewRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => testHandler.RemoveNews(requset));
        }

        /// <summary>
        /// 新闻 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetNewResponse> GetNews(GetNewRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => testHandler.GetNews(requset));
        }

        /// <summary>
        ///  新闻 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetNewDetailResponse> GetNewsDetail(GetNewDetailRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => testHandler.GetNewsDetail(requset));
        }

    }
}