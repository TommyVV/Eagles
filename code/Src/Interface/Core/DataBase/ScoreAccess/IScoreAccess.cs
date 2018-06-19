using System.Collections.Generic;
using Eagles.Base;
using Eagles.Application.Model.Common;

namespace Eagles.Interface.Core.DataBase.ScoreAccess
{
    public interface IScoreAccess : IInterfaceBase
    {
        List<ScoreExchange> GetScoreExchangeLs(int userId);

        List<UserRank> GetUserRank();

        List<BranchRank> GetBranchRank();
    }
}