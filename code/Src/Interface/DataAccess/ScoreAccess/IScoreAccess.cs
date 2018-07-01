using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.User;
using Eagles.DomainService.Model.Order;

namespace Eagles.Interface.DataAccess.ScoreAccess
{
    public interface IScoreAccess : IInterfaceBase
    {
        bool AppScoreExchange(TbOrder order, int userScore, int saleCount);

        List<TbUserScoreTrace> GetScoreExchangeLs(int userId);

        List<TbUserRank> GetUserRank();

        List<TbBranchRank> GetBranchRank();
    }
}