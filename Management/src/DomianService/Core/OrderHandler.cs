using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.DeliverGoods.Model;
using Eagles.Application.Model.DeliverGoods.Requset;
using Eagles.Application.Model.DeliverGoods.Response;
using Eagles.Application.Model.PartyMember.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.Order;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class OrderHandler : IOrderHandler
    {
        private readonly IOrderDataAccess dataAccess;

        private readonly IPartyMemberDataAccess memberDataAccess;

        public OrderHandler(IOrderDataAccess dataAccess, IPartyMemberDataAccess memberDataAccess)
        {
            this.dataAccess = dataAccess;
            this.memberDataAccess = memberDataAccess;
        }

        public bool EditOrders(EditDeliverGoodsRequest requset)
        {



            if (requset.OrderInfo.Count > 0 )
            {
                var mod = requset.OrderInfo.Select(x => new TbOrder
                {
                    OrderId = x.OrderId,
                    Address = x.Address,
                    ExpressId = x.ExpressId,
                    City = x.City,
                    District = x.District,
                    Province = x.Province,
                    OperId = 0,
                }).ToList();

                return dataAccess.EditOrder(mod) > 0;

            }

            throw new TransactionException("M03", "数据异常");

        }

        public GetDeliverGoodsDetailResponse GetOrdersDetail(GetDeliverGoodsDetailRequset requset)
        {


            var response = new GetDeliverGoodsDetailResponse();
            TbOrder detail = dataAccess.GetOrderDetail(requset);

            if (detail == null) throw new TransactionException("M01", "无业务数据");

            var userInfo = memberDataAccess.GetUserInfoDetail(new GetUserInfoDetailRequest { UserId = detail.UserId });

            if (userInfo == null)
            {
                throw new TransactionException("M01", "无业务数据");
            }
            response.Info = new DeliverGoodsDeatil
            {
                Address = detail.Address,
                OrderId = detail.OrderId,
                GoodsName = detail.ProdName,
                PlaceTime = detail.CreateTime.ToString("yyyy-MM-dd"),
                UserName = userInfo.Name,
                City = detail.City,
                Count = detail.Count,
                District = detail.District,
                Province = detail.Province,
                Score=detail.Score,
                OrderStatus=detail.OrderStatus
            };
            return response;
        }

        public GetDeliverGoodsResponse GetOrders(GetDeliverGoodsRequset requset)
        {
            var response = new GetDeliverGoodsResponse
            {
            };
            List<TbOrder> list = dataAccess.GetOrderList(requset);

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");

            response.List = list.Select(x => new DeliverGoods
            {
                Address = x.Address,
                OrderId = x.OrderId,
                GoodsName = x.ProdName,
                PlaceTime = x.CreateTime.ToString("yyyy-MM-dd"),
                OrderStatus=x.OrderStatus
            }).ToList();
            return response;
        }
    }
}
