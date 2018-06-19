using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.Application.Model.Common;
using Eagles.Interface.Core.DataBase.ScoreAccess;
using DomainModel = Eagles.DomainService.Model;

namespace Ealges.DomianService.DataAccess.ScoreData
{
    public class ScoreDataAccess : IScoreAccess
    {
        private readonly IDbManager dbManager;

        public ScoreDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<ScoreExchange> GetScoreExchangeLs(int userId)
        {
            dbManager.Query<DomainModel.Product.Product>("select ProdId,ProdName,Score,ImageUrl from eagles.tb_product ", new { });
            throw new System.NotImplementedException();
        }

        public List<UserRank> GetUserRank()
        {
            throw new System.NotImplementedException();
        }

        public List<BranchRank> GetBranchRank()
        {
            throw new System.NotImplementedException();
        }
    }
}