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

        public bool AppScoreExchange(TbOrder order, int userScore, int saleCount)
        {
            var commands = new List<TransactionCommand>()
            {
                new TransactionCommand()
                {
                    CommandString = @"insert into eagles.tb_order (OrgId,ProdId,ProdName,OrderStatus,Score,Count,UserId,Address,Province,City,District,CreateTime) value 
(@OrgId,@ProdId,@ProdName,@OrderStatus,@Score,@Count,@UserId,@Address,@Province,@City,@District,@CreateTime) ",
                    Parameter = order
                },
                new TransactionCommand()
                {
                    CommandString = "insert into eagles.tb_user_score_trace (OrgId,UserId,CreateTime,Score,Comment,OriScore) value (@OrgId,@UserId,@CreateTime,@Score,@Comment,@OriScore) ",
                    Parameter = new
                    {
                        OrgId = order.OrgId,
                        UserId = order.UserId,
                        CreateTime = order.CreateTime,
                        Score = order.Score * -1,
                        Comment = "兑换商品积分扣除",
                        OriScore = userScore
                    }
                },
                new TransactionCommand()
                {
                    CommandString = "update eagles.tb_product set Stock = Stock - @Stock, SaleCount = SaleCount + @SaleCount where ProdId = @ProdId ",
                    Parameter = new {Stock = saleCount, SaleCount = saleCount, ProdId = order.ProdId}
                }
            };
            return dbManager.ExcutedByTransaction(commands);
        }

        public List<TbUserScoreTrace> GetScoreExchangeLs(int userId)
        {
            return dbManager.Query<TbUserScoreTrace>(@"SELECT `tb_user_score_trace`.`OrgId`,
                `tb_user_score_trace`.`UserId`,
                `tb_user_score_trace`.`TraceId`,
                `tb_user_score_trace`.`CreateTime`,
                `tb_user_score_trace`.`Score`,
                `tb_user_score_trace`.`RewardsType`,
                `tb_user_score_trace`.`Comment`,
                `tb_user_score_trace`.`OriScore`
            FROM `eagles`.`tb_user_score_trace` where UserId = @UserId ", new { UserId =userId});
        }

        public List<TbUserRank> GetUserRank()
        {
            var sql = @"
select * from  eagles.tb_user_info  order by Score desc limit 10  ";
            return dbManager.Query<TbUserRank>(sql);
        }

        public List<TbBranchRank> GetBranchRank()
        {
            var sql = @"select (@i:=@i+1) as No,b.OrgName,count(a.UserId) as UserCount,a.Score from tb_user_info a 
join tb_org_info b on a.OrgId = b.OrgId ,(select @i:=0) as it group by b.OrgName,a.Score order by a.Score desc limit 10; ";
            return dbManager.Query<TbBranchRank>(sql);
        }
    }
}