using System;
using System.Linq;
using Eagles.Base;
using Eagles.Application.Model;
using Eagles.DomainService.Model.Order;
using Eagles.Interface.Core.Score;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.DataAccess.ScoreAccess;
using Eagles.Interface.DataAccess.ProductAccess;
using Eagles.Application.Model.Score.GetScoreRank;
using Eagles.Application.Model.Score.AppScoreExchange;
using Eagles.Application.Model.Score.GetScoreExchangeLs;

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
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var userInfo = util.GetUserInfo(tokens.UserId);
            if (userInfo == null)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            //查询商品
            var productInfo = iproductAccess.GetProductDetail(request.ProductId);
            if (productInfo == null)
                throw new TransactionException(MessageCode.ProductNotExists, MessageKey.ProductNotExists);
            var buyCount = request.Count; //购买数量
            var prodName = productInfo.ProdName; //商品名称
            var score = productInfo.Score; //商品积分
            var stock = productInfo.Stock; //库存
            var maxBuyCount = productInfo.MaxBuyCount; //每人最大购买数量
            var userCount = iproductAccess.GetOrderByProduct(request.ProductId, userInfo.UserId); //用户实际购买数量
            if (maxBuyCount < userCount || maxBuyCount < buyCount + userCount)
                throw new TransactionException(MessageCode.LimitedCount, MessageKey.LimitedCount);
            var userScore = userInfo.Score; //用户积分
            if (stock < buyCount)
                throw new TransactionException(MessageCode.NoInventory, MessageKey.NoInventory);
            if (userScore < score * request.Count)
                throw new TransactionException(MessageCode.InsufficientScore, MessageKey.InsufficientScore);
            var order = new TbOrder
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
            var exchange = iScoreAccess.AppScoreExchange(order, userScore, buyCount);
            //更新用户积分
            var result = util.EditUserScore(tokens.UserId, score * request.Count);
            if (result <= 0)
                throw new TransactionException(MessageCode.UpdateScoreFail, MessageKey.UpdateScoreFail);
            return response;
        }

        public GetScoreExchangeLsResponse GetScoreExchangeLs(GetScoreExchangeLsRequest request)
        {
            var response = new GetScoreExchangeLsResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            }
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var result = iScoreAccess.GetScoreExchangeLs(tokens.UserId);
            if (result != null && result.Count > 0)
            {
                response.ScoreList = result?.Select(x => new Application.Model.Common.UserScore()
                {
                    Score = x.Score,
                    CreateTime = x.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    RewardsType = x.RewardsType,
                    Comment = x.Comment,
                    OriScore = x.OriScore
                }).ToList();
            }
            else
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }

        public GetScoreRankResponse GetScoreRank(GetScoreRankRequest request)
        {
            var response = new GetScoreRankResponse();
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
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
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }
    }
}