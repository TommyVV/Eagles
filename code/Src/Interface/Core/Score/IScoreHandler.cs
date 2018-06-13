using System;
using Eagles.Base;
using Eagles.Application.Model.Curd.Score.GetScoreRank;
using Eagles.Application.Model.Curd.Score.GetScoreExchangeLs;

namespace Eagles.Interface.Core.Score
{
    public interface IScoreHandler:IInterfaceBase
    {
        GetScoreExchangeLsResponse GetScoreExchangeLs(GetScoreExchangeLsRequest request);
        GetScoreRankResponse GetScoreRank(GetScoreRankRequest request);
    }
}