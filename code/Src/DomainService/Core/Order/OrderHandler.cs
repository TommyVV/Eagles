using System.Linq;
using Eagles.Base;
using Eagles.Interface.Core.Order;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.DataAccess.OrderAccess;
using Eagles.Application.Model;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.Order.GetOrderLs;

namespace Eagles.DomainService.Core.Score
{
    public class OrderHandler : IOrderHandler
    {
        private readonly IOrderAccess iOrderAccess;
        private readonly IUtil util;

        public OrderHandler(IOrderAccess iOrderAccess, IUtil util)
        {
            this.iOrderAccess = iOrderAccess;
            this.util = util;
        }

        public GetOrderLsResponse GetOrderLs(GetOrderLsRequest request)
        {
            var response = new GetOrderLsResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var result = iOrderAccess.GetOrderLs(tokens.UserId, request.PageIndex, request.PageSize);
            if (result != null && result.Count > 0)
            {
                response.OrderList = result?.Select(x => new OrderLs()
                {
                    ProdId = x.ProdId,
                    ProdName = x.ProdName,
                    SmallImageUrl = x.SmallImageUrl,
                    OrderId = x.OrderId,
                    Score = x.Score,
                    CreateTime = x.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList();
            }
            else
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            return response;
        }
    }
}