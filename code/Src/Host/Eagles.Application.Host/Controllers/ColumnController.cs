using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.Column.Requset;
using Eagles.Application.Model.Column.Response;
using Eagles.Application.Model.Exercises.Requset;
using Eagles.Application.Model.Exercises.Response;
using Eagles.Interface.Core;

namespace Eagles.Host.Controllers
{
    public class ColumnController : ApiController
    {
        private IColumnHandler ColumnHandler;

        /// <summary>
        /// 编辑 栏目
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/EditColumn")]
        [HttpGet]
        public ResponseBase EditColumn(EditColumnRequset requset)
        {
            return ColumnHandler.EditColumn(requset);
        }

        /// <summary>
        /// 删除 栏目
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/RemoveColumn")]
        [HttpGet]
        public ResponseBase RemoveColumn(RemoveColumnRequset requset)
        {
            return ColumnHandler.RemoveColumn(requset);
        }

        /// <summary>
        /// 栏目 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/GetColumnDetail")]
        [HttpGet]
        public GetColumnDetailResponse GetColumnDetail(GetColumnDetailRequset requset)
        {
            return ColumnHandler.GetColumnDetail(requset);
        }

        /// <summary>
        /// 栏目 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/GetColumn")]
        [HttpGet]
        public GetColumnResponse GetColumn(GetColumnRequset requset)
        {
            return ColumnHandler.GetColumn(requset);
        }
    }
}