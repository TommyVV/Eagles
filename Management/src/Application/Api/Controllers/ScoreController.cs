using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.RollImage.Requset;
using Eagles.Application.Model.RollImage.Response;
using Eagles.Application.Model.ScoreRank.Requset;
using Eagles.Application.Model.ScoreRank.Response;
using Eagles.Application.Model.ScoreSetUp.Requset;
using Eagles.Application.Model.ScoreSetUp.Response;
using Eagles.Base;
using Eagles.Interface.Core;

using Eagles.Application.Host.Common;
namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 积分设置
    /// </summary>
    public class ScoreController : ApiController
    {
        private readonly IScoreHandler _ScoreHandler;

        public ScoreController(IScoreHandler testHandler)
        {
            this._ScoreHandler = testHandler;
        }


        /// <summary>
        /// 编辑/新增积分设置
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> EditScoreSetUp(EditScoreSetUpRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _ScoreHandler.EditScoreSetUp(requset));
        }

        /// <summary>
        /// 删除 积分
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> RemoveScoreSetUp(RemoveScoreSetUpRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _ScoreHandler.RemoveScoreSetUp(requset));
        }

        /// <summary>
        /// 积分 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetScoreSetUpDetailResponse> GetScoreSetUpDetail(GetScoreSetUpDetailRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _ScoreHandler.GetScoreSetUpDetail(requset));
        }

        /// <summary>
        /// 积分 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetScoreSetUpResponse> GetScoreSetUp(GetScoreSetUpRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _ScoreHandler.GetScoreSetUp(requset));
        }


        /// <summary>
        /// 积分排行 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetScoreRankDetailResponse> GetScoreRankDetail(GetScoreRankDetailRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _ScoreHandler.GetScoreRankDetail(requset));
        }

        /// <summary>
        /// 积分 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetScoreRankResponse> GetScoreRank(GetScoreRankRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _ScoreHandler.GetScoreRank(requset));
        }
    }
}