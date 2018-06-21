using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.Goods.Model;
using Eagles.Application.Model.Goods.Requset;
using Eagles.Application.Model.Goods.Response;
using Eagles.DomainService.Model.Product;
using Eagles.Interface.Core;
using Eagles.Interface.Core.DataBase;

namespace Eagles.DomainService.Core
{
    public class GoodsHandler : IGoodsHandler
    {
        private readonly IGoodsDataAccess dataAccess;

        public GoodsHandler(IGoodsDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }
        public ResponseBase EditGoods(EditGoodsRequset requset)
        {

            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };

            TB_PRODUCT mod;

            var now = DateTime.Now;
            if (requset.Info.GoodsId > 0)
            {
                mod = new TB_PRODUCT
                {
                    BeginTime=requset.Info.SellStartTime,
                    EndTime=requset.Info.SellEndTime,
                   // CreateTime=now,
                    EditTime=now,
                    HtmlDescription=requset.Info.Content,
                    ImageUrl =requset.Info.GoodsIcon,
                    MaxBuyCount=requset.Info.MaxExchangeNum,
                    OrgId=requset.OrgId,
                    Price=requset.Info.ReferePrice,
                    ProdId=requset.Info.GoodsId,
                    ProdName=requset.Info.GoodsName,
                    SaleCount=requset.Info.Sale,
                    Score=requset.Info.Score,
                    SmallImageUrl=requset.Info.GoodsImg,
                    Status=requset.Info.GoodsStatus,
                    Stock=requset.Info.Stock,
                    
                };

                int result = dataAccess.EditGoods(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
            }
            else
            {
                mod = new TB_PRODUCT
                {
                    BeginTime = requset.Info.SellStartTime,
                    EndTime = requset.Info.SellEndTime,
                    CreateTime = now,
                    EditTime = now,
                    HtmlDescription = requset.Info.Content,
                    ImageUrl = requset.Info.GoodsIcon,
                    MaxBuyCount = requset.Info.MaxExchangeNum,
                    OrgId = requset.OrgId,
                    Price = requset.Info.ReferePrice,
                    ProdId = requset.Info.GoodsId,
                    ProdName = requset.Info.GoodsName,
                    SaleCount = requset.Info.Sale,
                    Score = requset.Info.Score,
                    SmallImageUrl = requset.Info.GoodsImg,
                    Status = requset.Info.GoodsStatus,
                    Stock = requset.Info.Stock,
                   
                };

                int result = dataAccess.CreateGoods(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
            }

            return response;
        }

        public ResponseBase RemoveGoods(RemoveGoodsRequset requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };
            int result = dataAccess.RemoveGoods(requset);

            if (result > 0)
            {
                response.IsSuccess = true;
            }

            return response;
        }

        public GetGoodsDetailResponse GetGoodsDetail(GetGoodsDetailRequset requset)
        {
            var response = new GetGoodsDetailResponse
            {
                ErrorCode = "00",
                Message = "成功",
            };
            TB_PRODUCT detail = dataAccess.GetGoodsDetail(requset);
       

            if (detail == null) throw new Exception("无数据");

            response.Info = new GoodsDetail
            {
                Stock=detail.Stock,
                Score=detail.Score,
                Content=detail.HtmlDescription,
                GoodsIcon=detail.SmallImageUrl,
                GoodsId=detail.ProdId,
                GoodsImg=detail.ImageUrl,
                GoodsName=detail.ProdName,
                GoodsStatus=detail.Status,
                MaxExchangeNum=detail.MaxBuyCount,
                ReferePrice=detail.Price,
                Sale=detail.SaleCount,
                SellEndTime=detail.EndTime,
                SellStartTime=detail.BeginTime
            };
            return response;
        }

        public GetGoodsResponse GetGoods(GetGoodsRequest requset)
        {
            var response = new GetGoodsResponse
            {
                TotalCount = 0,
                ErrorCode = "00",
                Message = "成功",
            };
            List<TB_PRODUCT> list = dataAccess.GetNewsList(requset) ?? new List<TB_PRODUCT>();

            if (list.Count == 0) throw new Exception("无数据");

            response.List = list.Select(x => new Goods
            {
                Stock = x.Stock,
                Score = x.Score,
                GoodsId = x.ProdId,
                GoodsName = x.ProdName,
                GoodsStatus = x.Status,
            }).ToList();
            return response;
        }
    }
}
