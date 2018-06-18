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
    public class ColumnController : ApiController
    {
        private readonly IColumnHandler _columnHandler;

        public ColumnController(IColumnHandler testHandler)
        {
            this._columnHandler = testHandler;
        }



        /// <summary>
        /// 编辑 栏目
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/EditColumn")]
        [HttpPost]
        public ResponseBase EditColumn(EditColumnRequset requset)
        {
            return _columnHandler.EditColumn(requset);
        }

        /// <summary>
        /// 删除 栏目
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/RemoveColumn")]
        [HttpPost]
        public ResponseBase RemoveColumn(RemoveColumnRequset requset)
        {
            return _columnHandler.RemoveColumn(requset);
        }

        /// <summary>
        /// 栏目 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/GetColumnDetail")]
        [HttpPost]
        public GetColumnDetailResponse GetColumnDetail(GetColumnDetailRequset requset)
        {
            return _columnHandler.GetColumnDetail(requset);
        }

        /// <summary>
        /// 栏目 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/GetColumn")]
        [HttpPost]
        public GetColumnResponse GetColumn(GetColumnRequset requset)
        {
            return _columnHandler.GetColumn(requset);
        }
    }
}