﻿using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.Order;
using Eagles.Interface.DataAccess.OrderAccess;

namespace Ealges.DomianService.DataAccess.OrderData
{
    public class OrderDataAccess : IOrderAccess
    {
        private readonly IDbManager dbManager;

        public OrderDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<TbOrder> GetOrderLs(int userId, int pageIndex = 1, int pageSize = 10)
        {
            int pageIndexParameter = (pageIndex - 1) * pageSize;
            return dbManager.Query<TbOrder>(@"select a.OrderId,b.ProdId,b.ProdName,b.SmallImageUrl,a.Score,a.CreateTime from eagles.tb_order a
join eagles.tb_product b on a.ProdId = b.ProdId where a.UserId = @UserId limit @PageIndex, @PageSize ", new { UserId = userId , PageIndex = pageIndexParameter, PageSize = pageSize
            });
        }
    }
}