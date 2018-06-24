using System.Collections.Generic;
using Eagles.Application.Model.Common;
using Eagles.Base;
using Eagles.DomainService.Model.User;
using DomainModel = Eagles.DomainService.Model;

namespace Eagles.Interface.DataAccess.ScoreAccess
{
    public interface IScoreAccess : IInterfaceBase
    {
        bool AppScoreExchange(DomainModel.Order.TbOrder order, int userScore);

        List<TbUserScoreTrace> GetScoreExchangeLs(int userId);

        List<UserRank> GetUserRank();

        List<BranchRank> GetBranchRank();
    }
}