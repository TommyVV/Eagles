using System;
using System.Collections.Generic;
using System.Linq;
using Eagles.Application.Model.Goods.Model;
using Eagles.Application.Model.Goods.Requset;
using Eagles.Application.Model.Goods.Response;
using Eagles.Base;
using Eagles.Base.Cache;
using Eagles.DomainService.Model.Product;
using Eagles.DomainService.Model.User;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class GoodsHandler : IGoodsHandler
    {
        private readonly IGoodsDataAccess dataAccess;

        private readonly ICacheHelper cacheHelper;

        public GoodsHandler(IGoodsDataAccess dataAccess, ICacheHelper cacheHelper)
        {
            this.dataAccess = dataAccess;
            this.cacheHelper = cacheHelper;
        }

        public bool EditGoods(EditGoodsRequset requset)
        {

            TbProduct mod;
            var token = cacheHelper.GetData<TbUserToken>(requset.Token);
            var now = DateTime.Now;
            if (requset.Info.GoodsId > 0)
            {
                mod = new TbProduct
                {
                    BeginTime = requset.Info.SellStartTime,
                    EndTime = requset.Info.SellEndTime,
                    // CreateTime=now,
                    EditTime = now,
                    HtmlDescription = requset.Info.Content,
                    ImageUrl = requset.Info.GoodsIcon,
                    MaxBuyCount = requset.Info.MaxExchangeNum,
                    OrgId = token.OrgId,
                    Price = requset.Info.ReferePrice,
                    ProdId = requset.Info.GoodsId,
                    ProdName = requset.Info.GoodsName,
                    SaleCount = requset.Info.Sale,
                    Score = requset.Info.Score,
                    SmallImageUrl = requset.Info.GoodsImg,
                    Status = requset.Info.GoodsStatus,
                    Stock = requset.Info.Stock,

                };

                return dataAccess.EditGoods(mod) > 0;
            }

            mod = new TbProduct
            {
                BeginTime = requset.Info.SellStartTime,
                EndTime = requset.Info.SellEndTime,
                CreateTime = now,
                EditTime = now,
                HtmlDescription = requset.Info.Content,
                ImageUrl = requset.Info.GoodsIcon,
                MaxBuyCount = requset.Info.MaxExchangeNum,
                OrgId = token.OrgId,
                Price = requset.Info.ReferePrice,
                ProdId = requset.Info.GoodsId,
                ProdName = requset.Info.GoodsName,
                SaleCount = requset.Info.Sale,
                Score = requset.Info.Score,
                SmallImageUrl = requset.Info.GoodsImg,
                Status = requset.Info.GoodsStatus,
                Stock = requset.Info.Stock,

            };

            return dataAccess.CreateGoods(mod) > 0;

        }

        public bool RemoveGoods(RemoveGoodsRequset requset)
        {
            return dataAccess.RemoveGoods(requset) > 0;
        }

        public GetGoodsDetailResponse GetGoodsDetail(GetGoodsDetailRequset requset)
        {
            var response = new GetGoodsDetailResponse();
            TbProduct detail = dataAccess.GetGoodsDetail(requset);
       

            if (detail == null) throw new TransactionException("M01","无业务数据");

            response.Info = new GoodsDetail
            {
                Stock=detail.Stock,
                Score=detail.Score,
                Content=detail.HtmlDescription,
                GoodsIcon= detail.SmallImageUrl,
                GoodsId=detail.ProdId,
                GoodsImg= detail.ImageUrl,
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
               
               
            };
            var list = dataAccess.GetNewsList(requset) ?? new List<TbProduct>();

            if (list.Count == 0) throw new TransactionException("M01","无业务数据");

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
