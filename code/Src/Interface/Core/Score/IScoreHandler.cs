using Eagles.Base;
using Eagles.Application.Model.AppModel.Score.GetScoreRank;
using Eagles.Application.Model.AppModel.Score.AppScoreExchange;
using Eagles.Application.Model.AppModel.Score.GetScoreExchangeLs;

namespace Eagles.Interface.Core.Score
{
    public interface IScoreHandler : IInterfaceBase
    {
        AppScoreExchangeResponse AppScoreExchange(AppScoreExchangeRequest request);
        GetScoreExchangeLsResponse GetScoreExchangeLs(GetScoreExchangeLsRequest request);
        GetScoreRankResponse GetScoreRank(GetScoreRankRequest request);
    }
}