﻿using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.Application.Model.Common;
using Eagles.Interface.Core.DataBase.ScoreAccess;
using DomainModel = Eagles.DomainService.Model;
using Eagles.Base.DataBase.Modle;

namespace Ealges.DomianService.DataAccess.ScoreData
{
    public class ScoreDataAccess : IScoreAccess
    {
        private readonly IDbManager dbManager;

        public ScoreDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public bool AppScoreExchange(DomainModel.Order.Order order)
        {

            var commands = new List<TransactionCommand>()
            {
                new TransactionCommand()
                {
                    CommandString = @"insert into eagles.tb_order (OrgId,ProdId,ProdName,OrderStatus,Score,Count,Address,Province,City,District,CreateTime) value 
(@OrgId,@ProdId,@ProdName,@OrderStatus,@Score,@Count,@Address,@Province,@City,@District,@CreateTime)",
                    Parameter =  new {OrgId = order.OperId, ProdId = order.ProdId, ProdName = order.ProdName, OrderStatus = order.OrderStatus, Score = order.Score, Count = order.Count,
Address = order.Address, Province = order.Province,City=order.City,District=order.District,order.CreateTime}
                },
                new TransactionCommand()
                {
                    CommandString = "update tb_user_task set RewardsScore=@RewardsScore where TaskId=@TaskId",
                    Parameter =  new {RewardsScore = score, TaskId = taskId}
                },
            };
            return dbManager.ExcutedByTransaction(commands);
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