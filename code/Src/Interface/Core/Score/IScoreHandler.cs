using Eagles.Base;
using Eagles.Application.Model.Score.AppScoreExchange;
using Eagles.Application.Model.Score.GetScoreExchangeLs;
using Eagles.Application.Model.Score.GetScoreRank;

namespace Eagles.Interface.Core.Score
{
    public interface IScoreHandler : IInterfaceBase
    {
        AppScoreExchangeResponse AppScoreExchange(AppScoreExchangeRequest request);
        GetScoreExchangeLsResponse GetScoreExchangeLs(GetScoreExchangeLsRequest request);
        GetScoreRankResponse GetScoreRank(GetScoreRankRequest request);
    }
}