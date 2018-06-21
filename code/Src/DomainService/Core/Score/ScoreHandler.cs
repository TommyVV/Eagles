﻿using System.Linq;
using Eagles.Interface.Core.Score;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.Core.DataBase.ScoreAccess;
using Eagles.Application.Model.AppModel.Score.GetScoreRank;
using Eagles.Application.Model.AppModel.Score.AppScoreExchange;
using Eagles.Application.Model.AppModel.Score.GetScoreExchangeLs;
using Eagles.Interface.Core.DataBase.ProductAccess;
using System.Transactions;

namespace Eagles.DomainService.Core.Score
{
    public class ScoreHandler : IScoreHandler
    {
        private readonly IScoreAccess iScoreAccess;
        private readonly IProductAccess productAccess;
        private readonly IUtil util;

        public ScoreHandler(IScoreAccess iScoreAccess, IUtil util)
        {
            this.iScoreAccess = iScoreAccess;
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
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var userInfo = util.GetUserInfo(tokens.UserId);
            if (userInfo == null)
            {
                throw new TransactionException("用户不存在");
            }
            //查询商品
            var productInfo = productAccess.GetProductDetail(request.ProductId);
            if(productInfo == null)
            {
                response.ErrorCode = "96";
                response.Message = "商品信息不存在";
            }
            var prodName = productInfo.ProdName; //商品名称
            var score = productInfo.Score; //商品积分
            var userScore = userInfo.Score; //用户积分
            if(userScore < score)
                throw new TransactionException("用户积分不足");
            iScoreAccess.AppScoreExchange(tokens.UserId, request.ProductId, prodName, score, userScore);

            return response;
        }

        public GetScoreExchangeLsResponse GetScoreExchangeLs(GetScoreExchangeLsRequest request)
        {
            var response = new GetScoreExchangeLsResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = iScoreAccess.GetScoreExchangeLs(tokens.UserId);
            if (result != null && result.Count > 0)
            {
                response.ScoreList = result?.Select(x => new Application.Model.Common.ScoreExchange()
                {
                    ProdName = x.ProdName,
                    Score = x.ProdName,
                    ExchangDate = x.ExchangDate,
                    OrderId = x.OrderId,
                    Address = x.Address
                }).ToList();
                response.ErrorCode = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查无数据";
            }
            return response;
        }

        public GetScoreRankResponse GetScoreRank(GetScoreRankRequest request)
        {
            var response = new GetScoreRankResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var userResult = iScoreAccess.GetUserRank();
            if (userResult != null && userResult.Count > 0)
            {
                //response.UserRank = userResult?.Select(x => new Application.Model.Common.Activity()
                //{

                //}).ToList();
            }
            else
            {
                response.UserRank = null;
            }
            var branckResult = iScoreAccess.GetBranchRank();
            if (branckResult != null && branckResult.Count > 0)
            {
                //response.BranchRank = userResult?.Select(x => new Application.Model.Common.Activity()
                //{

                //}).ToList();
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查无数据";
            }
            return response;
        }
    }
}