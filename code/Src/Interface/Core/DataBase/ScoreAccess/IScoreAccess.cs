using System.Collections.Generic;
using Eagles.Base;
using Eagles.Application.Model.Common;
using DomainModel = Eagles.DomainService.Model;

namespace Eagles.Interface.Core.DataBase.ScoreAccess
{
    public interface IScoreAccess : IInterfaceBase
    {
        bool AppScoreExchange(DomainModel.Order.TbOrder order, int userScore);

        List<ScoreExchange> GetScoreExchangeLs(int userId);

        List<UserRank> GetUserRank();

        List<BranchRank> GetBranchRank();
    }
}