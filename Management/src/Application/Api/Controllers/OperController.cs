using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.Operator.Requset;
using Eagles.Application.Model.Operator.Response;
using Eagles.Base;
using Eagles.Interface.Core;

using Eagles.Application.Host.Common;
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
        public ResponseFormat<bool> EditOperator(EditOperatorRequset requset)
        {
             return ApiActuator.Runing(requset, (requset1) =>_OperatorHandler.EditOper(requset));
        }

        /// <summary>
        /// 删除 管理员
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> RemoveOperator(RemoveOperatorRequset requset)
        {
             return ApiActuator.Runing(requset, (requset1) =>_OperatorHandler.RemoveOper(requset));
        }

        /// <summary>
        /// 管理员 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetOperatorDetailResponse> GetOperatorDetail(GetOperatorDetailRequset requset)
        {
             return ApiActuator.Runing(requset, (requset1) =>_OperatorHandler.GetOperDetail(requset));
        }

        /// <summary>
        /// 管理员 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetOperatorResponse> GetOperator(GetOperatorRequset requset)
        {
             return ApiActuator.Runing(requset, (requset1) =>_OperatorHandler.GetOperList(requset));
        }
    }
}