using System.Linq;
using Eagles.Interface.Core.Score;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.Core.DataBase.ScoreAccess;
using Eagles.Application.Model.Curd.Score.GetScoreRank;
using Eagles.Application.Model.Curd.Score.GetScoreExchangeLs;

namespace Eagles.DomainService.Core.Score
{
    public class ScoreHandler : IScoreHandler
    {
        private readonly IScoreAccess iScoreAccess;
        private readonly IUtil util;

        public ScoreHandler(IScoreAccess iScoreAccess, IUtil util)
        {
            this.iScoreAccess = iScoreAccess;
            this.util = util;
        }

        public GetScoreExchangeLsResponse GetScoreExchangeLs(GetScoreExchangeLsRequest request)
        {
            var response = new GetScoreExchangeLsResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = iScoreAccess.GetScoreExchangeLs(tokens.UserId);
            if (result != null && result.Count > 0)
            {
                response.ScoreList = result?.Select(x => new Application.Model.Common.ScoreExchange()
                {
                    ProdName = x.ProdName,
                    Score = x.ProdName,
                    ExchangDate = x.ExchangDate,
                    OrderId = x.OrderId,
                    Address = x.Address
                }).ToList();
                response.ErrorCode = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查无数据";
            }
            return response;
        }

        public GetScoreRankResponse GetScoreRank(GetScoreRankRequest request)
        {
            var response = new GetScoreRankResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var userResult = iScoreAccess.GetUserRank();
            if (userResult != null && userResult.Count > 0)
            {
                //response.UserRank = userResult?.Select(x => new Application.Model.Common.Activity()
                //{

                //}).ToList();
            }
            else
            {
                response.UserRank = null;
            }
            var branckResult = iScoreAccess.GetBranchRank();
            if (branckResult != null && branckResult.Count > 0)
            {
                //response.BranchRank = userResult?.Select(x => new Application.Model.Common.Activity()
                //{

                //}).ToList();
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查无数据";
            }
            return response;
        }
    }
}