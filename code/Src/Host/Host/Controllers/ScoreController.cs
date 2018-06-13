using System;
using System.Web.Http;
using Eagles.Interface.Core.Score;
using Eagles.Application.Model.Curd.Score.GetScoreRank;
using Eagles.Application.Model.Curd.Score.GetScoreExchangeLs;

namespace Eagles.Host.Controllers
{
    public class ScoreController : ApiController
    {
        private IScoreHandler scoreHandler;

        /// <summary>
        /// 积分兑换流水查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetScoreExchangeLs")]
        [HttpGet]
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
        [HttpGet]
        public GetScoreRankResponse GetScoreRank(GetScoreRankRequest request)
        {
            return scoreHandler.GetScoreRank(request);
        }
    }
}