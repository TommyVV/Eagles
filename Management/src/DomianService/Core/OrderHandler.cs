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
using Eagles.DomainService.Model.Order;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class OrderHandler: IOrderHandler
    {
        private readonly IOrderDataAccess dataAccess;

        private readonly IPartyMemberDataAccess memberDataAccess;

        public OrderHandler(IOrderDataAccess dataAccess, IPartyMemberDataAccess memberDataAccess)
        {
            this.dataAccess = dataAccess;
            this.memberDataAccess = memberDataAccess;
        }

        public ResponseBase EditOrders(EditDeliverGoodsRequest requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };


            if (requset.OrderInfo.Count > 0)
            {
                var mod = requset.OrderInfo.Select(x => new TbOrder
                {
                    OrderId = x.OrderId,
                    Address = x.Address,
                    ExpressId = x.ExpressId,
                    OperId=0,
                }).ToList();

                int result = dataAccess.EditOrder(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
            }

            return response;
        }

        public GetDeliverGoodsDetailResponse GetOrdersDetail(GetDeliverGoodsDetailRequset requset)
        {


            var response = new GetDeliverGoodsDetailResponse
            {
                ErrorCode = "00",
                Message = "成功",
            };
            TbOrder detail = dataAccess.GetOrderDetail(requset);


            if (detail == null) throw new Exception("无数据");

            var userInfo = memberDataAccess.GetUserInfoDetail(new GetUserInfoDetailRequest {UserId = detail.UserId});
            response.Info = new DeliverGoods
            {
                Address = detail.Address,
                OrderId = detail.OrderId,
                GoodsName = detail.ProdName,
                PlaceTime = detail.CreateTime,
                UserName = userInfo.Name,
            };
            return response;
        }

        public GetDeliverGoodsResponse GetOrders(GetDeliverGoodsRequset requset)
        {
            var response = new GetDeliverGoodsResponse
            {
                ErrorCode = "00",
                Message = "成功",
            };
            List<TbOrder> list = dataAccess.GetOrderList(requset);

            if (list.Count == 0) throw new Exception("无数据");

            response.List = list.Select(x => new DeliverGoods
            {
                Address = x.Address,
                OrderId = x.OrderId,
                GoodsName = x.ProdName,
                PlaceTime = x.CreateTime,
            }).ToList();
            return response;          
        }
    }
}
