using Eagles.Base;
using Eagles.Application.Model.AppModel.Score.GetScoreRank;
using Eagles.Application.Model.AppModel.Score.GetScoreExchangeLs;

namespace Eagles.Interface.Core.Score
{
    public interface IScoreHandler : IInterfaceBase
    {
        GetScoreExchangeLsResponse GetScoreExchangeLs(GetScoreExchangeLsRequest request);
        GetScoreRankResponse GetScoreRank(GetScoreRankRequest request);
    }
}