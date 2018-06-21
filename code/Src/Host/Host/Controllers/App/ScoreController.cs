using System.Web.Http;
using Eagles.Interface.Core.Score;
using Eagles.Application.Model.AppModel.Score.GetScoreRank;
using Eagles.Application.Model.AppModel.Score.GetScoreExchangeLs;
using Eagles.Application.Model.AppModel.Score.AppScoreExchange;

namespace Eagles.Application.Host.Controllers.App
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
        public AppScoreExchangeResponse AppScoreExchange(AppScoreExchangeRequest request)
        {
            return scoreHandler.AppScoreExchange(request);
        }

        /// <summary>
        /// 积分兑换流水查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetScoreExchangeLs")]
        [HttpPost]
        public GetScoreExchangeLsResponse GetScoreExchangeLs(GetScoreExchangeLsRequest request)
        {
            return scoreHandler.GetScoreExchangeLs(request);
        }

        /// <summary>
        /// 积分排行查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetScoreRank")]
        [HttpPost]
        public GetScoreRankResponse GetScoreRank(GetScoreRankRequest request)
        {
            return scoreHandler.GetScoreRank(request);
        }
    }
}