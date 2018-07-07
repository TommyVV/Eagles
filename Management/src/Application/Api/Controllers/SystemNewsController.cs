using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.SystemMessage.Requset;
using Eagles.Application.Model.SystemMessage.Response;
using Eagles.Base;
using Eagles.Interface.Core;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemNewsController : ApiController
    {
        private readonly ISystemNewsHandler _columnHandler;

        public SystemNewsController(ISystemNewsHandler testHandler)
        {
            this._columnHandler = testHandler;
        }



        /// <summary>
        /// 编辑 系统消息
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> EditSystemNews(EditSystemNewsRequset requset)
        {
            return ApiActuator.Runing(() =>_columnHandler.EditSystemNews(requset));
        }

        /// <summary>
        /// 删除 系统消息
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> RemoveSystemNews(RemoveSystemNewsRequset requset)
        {
            return ApiActuator.Runing(() =>_columnHandler.RemoveSystemNews(requset));
        }

        /// <summary>
        /// 系统消息 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetSystemNewsDetailResponse> GetSystemNewsDetail(GetSystemNewsDetailRequset requset)
        {
            return ApiActuator.Runing(() =>_columnHandler.GetSystemNewsDetail(requset));
        }

        /// <summary>
        /// 系统消息 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetSystemNewsResponse> GetSystemNews(GetSystemNewsRequset requset)
        {
            return ApiActuator.Runing(() =>_columnHandler.SystemNews(requset));
        }
    }
}