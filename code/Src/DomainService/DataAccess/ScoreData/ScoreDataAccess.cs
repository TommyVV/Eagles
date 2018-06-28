using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.Base.DataBase.Modle;
using Eagles.DomainService.Model.Order;
using Eagles.DomainService.Model.User;
using Eagles.Application.Model.Common;
using Eagles.Interface.DataAccess.ScoreAccess;

namespace Ealges.DomianService.DataAccess.ScoreData
{
    public class ScoreDataAccess : IScoreAccess
    {
        private readonly IDbManager dbManager;

        public ScoreDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public bool AppScoreExchange(TbOrder order, int userScore)
        {
            var commands = new List<TransactionCommand>()
            {
                new TransactionCommand()
                {
                    CommandString = @"insert into eagles.tb_order (OrgId,ProdId,ProdName,OrderStatus,Score,Count,Address,Province,City,District,CreateTime) value 
(@OrgId,@ProdId,@ProdName,@OrderStatus,@Score,@Count,@Address,@Province,@City,@District,@CreateTime) ",
                    Parameter =  new {OrgId = order.OrgId, ProdId = order.ProdId, ProdName = order.ProdName, OrderStatus = order.OrderStatus, Score = order.Score, Count = order.Count,
Address = order.Address, Province = order.Province, City = order.City, District = order.District, CreateTime = order.CreateTime}
                },
                new TransactionCommand()
                {
                    CommandString = "insert into eagles.tb_user_score_trace (OrgId,UserId,CreateTime,Score,Comment,OriScore) value (@OrgId,@UserId,@CreateTime,@Score,@Comment,@OriScore) ",
                    Parameter =  new {OrgId = order.OrgId, UserId = order.UserId, CreateTime = order.CreateTime, Score = order.Score, Comment = "兑换商品积分扣除", OriScore = userScore}
                }
            };
            return dbManager.ExcutedByTransaction(commands);
        }

        public List<TbUserScoreTrace> GetScoreExchangeLs(int userId)
        {
            return dbManager.Query<TbUserScoreTrace>("select OrgId, UserId, TraceId, CreateTime, Score, RewardsType, Comment, OriScore from eagles.tb_user_score_trace where UserId = @UserId ", new { UserId =userId});
        }

        public List<UserRank> GetUserRank()
        {
            var sql = @"select a.Name, b.OrgName,a.Score from tb_user_info a join tb_org_info b on a.OrgId = b.OrgId order by a.Score desc limit 10 ";
            throw new System.NotImplementedException();
        }

        public List<BranchRank> GetBranchRank()
        {
            var sql = @"select sum(score) as ss from tb_user_info group by orgId order by ss desc limit 10 ";
            throw new System.NotImplementedException();
        }
    }
}