using System.Web.Http;
using Eagles.Application.Model.Branch.Requset;
using Eagles.Application.Model.Branch.Response;
using Eagles.Interface.Core;

using Eagles.Application.Host.Common;
namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class BranchController : ApiController
    {
        private readonly IBranchHandler _columnHandler;

        public BranchController(IBranchHandler testHandler)
        {
            this._columnHandler = testHandler;
        }

        /// <summary>
        /// 编辑 支部
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> EditBranch(EditBranchRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _columnHandler.EditBranch(requset));
        }

        /// <summary>
        /// 删除 支部
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> RemoveBranch(RemoveBranchRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _columnHandler.RemoveBranch(requset));
        }

        /// <summary>
        /// 支部 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetBranchDetailResponse> GetBranchDetail(GetBranchDetailRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _columnHandler.GetBranchDetail(requset));
        }

        /// <summary>
        /// 支部 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetBranchResponse> GetBranch(GetBranchRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _columnHandler.Branch(requset));
        }
    }
}