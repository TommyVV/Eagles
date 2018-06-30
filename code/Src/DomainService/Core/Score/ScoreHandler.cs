﻿using System;
using System.Linq;
using Eagles.Base;
using Eagles.Interface.Core.Score;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.DataAccess.ScoreAccess;
using Eagles.Interface.DataAccess.ProductAccess;
using Eagles.Application.Model.Score.GetScoreRank;
using Eagles.Application.Model.Score.AppScoreExchange;
using Eagles.Application.Model.Score.GetScoreExchangeLs;
using DomainModel = Eagles.DomainService.Model;

namespace Eagles.DomainService.Core.Score
{
    public class ScoreHandler : IScoreHandler
    {
        private readonly IScoreAccess iScoreAccess;
        private readonly IProductAccess iproductAccess;
        private readonly IUtil util;

        public ScoreHandler(IScoreAccess iScoreAccess, IProductAccess iproductAccess, IUtil util)
        {
            this.iScoreAccess = iScoreAccess;
            this.iproductAccess = iproductAccess;
            this.util = util;
        }

        /// <summary>
        /// 积分兑换接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AppScoreExchangeResponse AppScoreExchange(AppScoreExchangeRequest request)
        {
            var response = new AppScoreExchangeResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                throw new TransactionException("96", "获取Token失败");
            }

            var userInfo = util.GetUserInfo(tokens.UserId);
            if (userInfo == null)
            {
                throw new TransactionException("01", "用户不存在");
            }

            //查询商品
            var productInfo = iproductAccess.GetProductDetail(request.ProductId);
            if (productInfo == null)
            {
                throw new TransactionException("96", "商品信息不存在");
            }

            var prodName = productInfo.ProdName; //商品名称
            var score = productInfo.Score; //商品积分
            var userScore = userInfo.Score; //用户积分
            if (userScore < score * request.Count)
                throw new TransactionException("01", "用户积分不足");



            var order = new DomainModel.Order.TbOrder
            {
                OrgId = tokens.OrgId,
                ProdId = request.ProductId,
                ProdName = prodName,
                OrderStatus = 0,
                Score = score,
                Count = request.Count,
                Address = request.Address,
                Province = request.Province,
                City = request.City,
                District = request.District,
                CreateTime = DateTime.Now,
                UserId = userInfo.UserId
            };

            //订单表、流水表
            iScoreAccess.AppScoreExchange(order, userScore);
            //更新用户积分
            var result = util.EditUserScore(tokens.UserId, score * request.Count);
            if (result <= 0)
            {
                throw new TransactionException("01", "用户积分不足");
            }

            return response;
        }

        public GetScoreExchangeLsResponse GetScoreExchangeLs(GetScoreExchangeLsRequest request)
        {
            var response = new GetScoreExchangeLsResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.Code = "96";
                response.Message = "获取Token失败";
                return response;
            }
            if (request.AppId <= 0)
                throw new TransactionException("01", "AppId不允许为空");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
            var result = iScoreAccess.GetScoreExchangeLs(tokens.UserId);
            if (result != null && result.Count > 0)
            {
                response.ScoreList = result?.Select(x => new Application.Model.Common.ScoreExchange()
                {
                    Score = x.Score,
                    CreateTime = x.CreateTime,
                    RewardsType = x.RewardsType,
                    Comment = x.Comment,
                    OriScore = x.OriScore
                }).ToList();
                response.Code = "00";
                response.Message = "查询成功";
            }
            else
            {
                throw new TransactionException("96", "查无数据");
            }
            return response;
        }

        public GetScoreRankResponse GetScoreRank(GetScoreRankRequest request)
        {
            var response = new GetScoreRankResponse();
            if (request.AppId <= 0)
                throw new TransactionException("01", "AppId不允许为空");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
            var userResult = iScoreAccess.GetUserRank();
            if (userResult != null && userResult.Count > 0)
            {
                response.UserRank = userResult?.Select(x => new Application.Model.Common.UserRank()
                {
                    Rank = x.No,
                    Name = x.Name,
                    BranchName = x.OrgName,
                    Score = x.Score
                }).ToList();
            }
            else
            {
                response.UserRank = null;
            }
            var branckResult = iScoreAccess.GetBranchRank();
            if (branckResult != null && branckResult.Count > 0)
            {
                response.BranchRank = branckResult?.Select(x => new Application.Model.Common.BranchRank()
                {
                    Rank = x.No,
                    Branch = x.OrgName,
                    UserCount = x.UserCount,
                    Score = x.Score
                }).ToList();
            }
            else
            {
                response.Code = "96";
                response.Message = "查无数据";
            }
            return response;
        }
    }
}