using System.Web.Http;
using Eagles.Application.Model.Score.AppScoreExchange;
using Eagles.Application.Model.Score.GetScoreExchangeLs;
using Eagles.Application.Model.Score.GetScoreRank;
using Eagles.Base;
using Eagles.Interface.Core.Score;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 积分查询
    /// </summary>
    [ValidServiceToken]
    public class ScoreController : ApiController
    {
        private IScoreHandler scoreHandler;

        public ScoreController(IScoreHandler scoreHandler)
        {
            this.scoreHandler = scoreHandler;
        }

        /// <summary>
        /// 积分兑换接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseFormat<AppScoreExchangeResponse> AppScoreExchange(AppScoreExchangeRequest request)
        {
            return ApiActuator.Runing(() => scoreHandler.AppScoreExchange(request));
            //return scoreHandler.AppScoreExchange(request);
        }

        /// <summary>
        /// 积分兑换流水查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetScoreExchangeLsResponse> GetScoreExchangeLs(GetScoreExchangeLsRequest request)
        {
            return ApiActuator.Runing(() => scoreHandler.GetScoreExchangeLs(request));
           // return scoreHandler.GetScoreExchangeLs(request);
        }

        /// <summary>
        /// 积分排行查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetScoreRankResponse> GetScoreRank(GetScoreRankRequest request)
        {
            return ApiActuator.Runing(() => scoreHandler.GetScoreRank(request));
      
        }
    }
}