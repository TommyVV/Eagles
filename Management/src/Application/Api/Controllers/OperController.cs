using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.Operator.Requset;
using Eagles.Application.Model.Operator.Response;
using Eagles.Interface.Core;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// Operator
    /// </summary>
    public class OperatorController : ApiController
    {
        private readonly IOperHandler _OperatorHandler;

        public OperatorController(IOperHandler testHandler)
        {
            this._OperatorHandler = testHandler;
        }


        /// <summary>
        /// 编辑 管理员
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseBase EditOperator(EditOperatorRequset requset)
        {
            return _OperatorHandler.EditOper(requset);
        }

        /// <summary>
        /// 删除 管理员
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseBase RemoveOperator(RemoveOperatorRequset requset)
        {
            return _OperatorHandler.RemoveOper(requset);
        }

        /// <summary>
        /// 管理员 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public GetOperatorDetailResponse GetOperatorDetail(GetOperatorDetailRequset requset)
        {
            return _OperatorHandler.GetOperDetail(requset);
        }

        /// <summary>
        /// 管理员 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public GetOperatorResponse GetOperator(GetOperatorRequset requset)
        {
            return _OperatorHandler.GetOperList(requset);
        }
    }
}