using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.Column.Requset;
using Eagles.Application.Model.Column.Response;
using Eagles.Interface.Core;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ModuleController : ApiController
    {
        private readonly IModuleHandler _moduleHandler;

        public ModuleController(IModuleHandler moduleHandler)
        {
            this._moduleHandler = moduleHandler;
        }



        /// <summary>
        /// 编辑 栏目
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseBase EditColumn(EditColumnRequset requset)
        {
            return _moduleHandler.EditColumn(requset);
        }

        /// <summary>
        /// 删除 栏目
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseBase RemoveColumn(RemoveColumnRequset requset)
        {
            return _moduleHandler.RemoveColumn(requset);
        }

        /// <summary>
        /// 栏目 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public GetColumnDetailResponse GetColumnDetail(GetColumnDetailRequset requset)
        {
            return _moduleHandler.GetColumnDetail(requset);
        }

        /// <summary>
        /// 栏目 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public GetColumnResponse GetColumn(GetColumnRequset requset)
        {
            return _moduleHandler.GetColumn(requset);
        }
    }
}