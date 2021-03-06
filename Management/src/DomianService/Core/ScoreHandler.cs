﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.PartyMember.Requset;
using Eagles.Application.Model.RollImage.Model;
using Eagles.Application.Model.ScoreRank.Model;
using Eagles.Application.Model.ScoreRank.Requset;
using Eagles.Application.Model.ScoreRank.Response;
using Eagles.Application.Model.ScoreSetUp.Model;
using Eagles.Application.Model.ScoreSetUp.Requset;
using Eagles.Application.Model.ScoreSetUp.Response;
using Eagles.Base;
using Eagles.Base.Cache;
using Eagles.Base.Utility;
using Eagles.DomainService.Model.Score;
using Eagles.DomainService.Model.User;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class ScoreHandler : IScoreHandler
    {
        private readonly IScoreDataAccess dataAccess;

        private readonly IPartyMemberDataAccess UserdataAccess;

        private readonly ICacheHelper cacheHelper;

        public ScoreHandler(IScoreDataAccess dataAccess, IPartyMemberDataAccess userdataAccess, ICacheHelper cacheHelper)
        {
            this.dataAccess = dataAccess;
            UserdataAccess = userdataAccess;
            this.cacheHelper = cacheHelper;
        }

        public bool EditScoreSetUp(EditScoreSetUpRequset requset)
        {

            TbRewardScore mod;
            var now = DateTime.Now;
            var tokenInfo = cacheHelper.GetData<TbUserToken>(requset.Token);
            if (requset.Info.ScoreSetUpId > 0)
            {
                mod = new TbRewardScore
                {
                    BranchId = tokenInfo.BranchId,
                    OrgId = tokenInfo.OrgId,
                    KeyWord = string.Join(",", requset.Info.Keyword.ToArray()),
                    LearnTime = requset.Info.LearnTime,
                    RewardId = requset.Info.ScoreSetUpId,
                    RewardType = requset.Info.RewardType,
                    Score = requset.Info.Score,
                    WordCount = requset.Info.WordCount,

                };

                return dataAccess.EditScoreSetUp(mod) > 0;


            }
            else
            {
                mod = new TbRewardScore
                {
                    BranchId = tokenInfo.BranchId,
                    OrgId = tokenInfo.OrgId,
                    KeyWord = string.Join(",", requset.Info.Keyword.ToArray()),
                    LearnTime = requset.Info.LearnTime,
                    RewardId = requset.Info.ScoreSetUpId,
                    RewardType = requset.Info.RewardType,
                    Score = requset.Info.Score,
                    WordCount = requset.Info.WordCount,

                };
                return dataAccess.CreateScoreSetUp(mod) > 0;
            }
        }

        public bool RemoveScoreSetUp(RemoveScoreSetUpRequset requset)
        {
            return dataAccess.RemoveScoreSetUp(requset) > 0;
        }

        public GetScoreSetUpDetailResponse GetScoreSetUpDetail(GetScoreSetUpDetailRequset requset)
        {

            var response = new GetScoreSetUpDetailResponse
            {

            };
            var detail = dataAccess.GetScoreSetUpDetail(requset);

            if (detail == null) throw new TransactionException("M01", "无业务数据");

            response.Info = new ScoreSetUpInfo
            {
                Keyword = string.IsNullOrWhiteSpace(detail.KeyWord) ? new List<string>() : detail.KeyWord.Split(',').ToList(),
                //  OperationType = detail.OrgId,
                // KeyWord = string.Join(",", requset.Info.Keyword.ToArray()),
                LearnTime = detail.LearnTime,
                ScoreSetUpId = detail.RewardId,
                RewardType = detail.RewardType,
                Score = detail.Score,
                WordCount = detail.WordCount,
                // ScoreCount=detail.
                // Status=detail.s
            };
            return response;

        }

        public GetScoreSetUpResponse GetScoreSetUp(GetScoreSetUpRequset requset)
        {
            var response = new GetScoreSetUpResponse
            {
                TotalCount = 0,

            };
            List<TbRewardScore> list = dataAccess.GetScoreSetUps(requset, out int totalCount) ?? new List<TbRewardScore>();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");

            response.TotalCount = totalCount;
            response.List = list.Select(x => new ScoreSetUpInfo
            {
                Keyword = string.IsNullOrWhiteSpace(x.KeyWord)?new List<string>(): x.KeyWord.Split(',').ToList(),
                //  OperationType = detail.OrgId,
                // KeyWord = string.Join(",", requset.Info.Keyword.ToArray()),
                LearnTime = x.LearnTime,
                ScoreSetUpId = x.RewardId,
                RewardType = x.RewardType,
                Score = x.Score,
                WordCount = x.WordCount,
            }).ToList();
            return response;

        }

        public GetScoreRankDetailResponse GetScoreRankDetail(GetScoreRankDetailRequset requset)
        {
            var response = new GetScoreRankDetailResponse
            {

            };
            var detail = UserdataAccess.GetUserInfoDetail(new GetUserInfoDetailRequest()
            {
                UserId = requset.UserId,
                Token = requset.Token
            });

            if (detail == null) throw new TransactionException("M01", "无业务数据");


            var list = dataAccess.GetScoreTrace(requset.UserId);

            response.Info = list.Select(x => new UserScoreTrace
            {
                Comment = x.Comment,
                CreateTime = x.CreateTime.FormartDatetime(),
                RewardsType = x.RewardsType,
                Score = x.Score,
            }).ToList();
            return response;
        }

        public GetScoreRankResponse GetScoreRank(GetScoreRankRequset requset)
        {
            var response = new GetScoreRankResponse
            {
                TotalCount = 0,

            };
            List<TbUserInfo> list = UserdataAccess.GetUserInfoList(new GetPartyMemberRequest()
            {
                UserName = requset.UserName,
                //OrgId = requset.OrgId,
                //BranchId = requset.BranchId,
                //EndTime = requset.EndTime,
                //PageNumber = requset.PageNumber,
                //PageSize = requset.PageSize,
                //StartTime = requset.StartTime,todo
                Token = requset.Token,
            }, out var totalCount) ?? new List<TbUserInfo>();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");

            response.TotalCount = totalCount;
            response.List = list.Select(x => new ScoreRankInfo
            {
                Score = x.Score,
                UserId = x.UserId,
                UserIdentity = x.MemberType,
                UserName = x.Name,
               // UseScore = ""

            }).ToList();
            return response;
        }
    }
}
