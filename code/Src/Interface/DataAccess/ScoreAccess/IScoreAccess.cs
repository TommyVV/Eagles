using System.Collections.Generic;
using Eagles.Application.Model.Common;
using Eagles.Base;
using DomainModel = Eagles.DomainService.Model;

namespace Eagles.Interface.DataAccess.ScoreAccess
{
    public interface IScoreAccess : IInterfaceBase
    {
        bool AppScoreExchange(DomainModel.Order.TbOrder order, int userScore);

        List<ScoreExchange> GetScoreExchangeLs(int userId);

        List<UserRank> GetUserRank();

        List<BranchRank> GetBranchRank();
    }
}