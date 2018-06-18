using System.Web.Http;
using Eagles.Interface.Core.Score;
using Eagles.Application.Model.Curd.Score.GetScoreRank;
using Eagles.Application.Model.Curd.Score.GetScoreExchangeLs;

namespace Eagles.Host.Controllers
{
    /// <summary>
    /// 积分查询
    /// </summary>
    public class ScoreController : ApiController
    {
        private IScoreHandler scoreHandler;

        public ScoreController(IScoreHandler scoreHandler)
        {
            this.scoreHandler = scoreHandler;
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