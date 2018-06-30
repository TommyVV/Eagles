﻿using System;
using System.Linq;
using Eagles.Base;
using Eagles.Interface.Core.News;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.DataAccess.NewsDa;
using Eagles.Interface.DataAccess.UserArticle;
using Eagles.Application.Model.News.CreateNews;
using Eagles.Application.Model.News.GetNews;
using Eagles.Application.Model.News.GetModuleNews;
using Eagles.Application.Model.News.GetNewsDetail;
using Eagles.DomainService.Model.User;

namespace Eagles.DomainService.Core.News
{
    public class NewsHandler : INewsHandler
    {
        private readonly IArticleDataAccess articleData;

        private readonly INewsDa newsDa;

        private readonly IUtil util;

        public NewsHandler(IArticleDataAccess articleData, IUtil util, INewsDa newsDa)
        {
            this.articleData = articleData;
            this.util = util;
            this.newsDa = newsDa;
        }

        public CreateArticleResponse CreateArticle(CreateArticleRequest request)
        {
            var response = new CreateArticleResponse();
            if (request.AppId <= 0)
                throw new TransactionException("01", "AppId不允许为空");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");

            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.Code = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var userInfo = util.GetUserInfo(tokens.UserId);
            if (userInfo == null)
            {
                throw new TransactionException("01", "用户不存在");
            }
            var newsInfo = new TbUserNews()
            {
                OrgId = tokens.OrgId,
                BranchId = tokens.BranchId,
                UserId = tokens.UserId,
                Title = request.NewsTitle,
                HtmlContent = request.NewsContent,
                NewsType = request.NewsType,
                Status = "-1",
                CreateTime = DateTime.Now,
                RewardsScore = request.RewardsScore,
                OrgReview = "-1",
                BranchReview = "-1"
            };
            var result = articleData.CreateArticle(newsInfo);
            if (result > 0)
            {
                response.Code = "00";
                response.Message = "成功";
            }
            else
            {
                response.Code = "96";
                response.Message = "失败";
            }
            return response;
        }

        public GetNewsResponse GetUserArticle(GetNewsRequest request)
        {
            if (request.AppId <= 0)
                throw new TransactionException("01", "AppId不允许为空");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                return null;// todo error
            }
            
            var news = articleData.GetUserNewsList(tokens.UserId);
            //convert news 
            return new GetNewsResponse()
            {
                NewsList = news.Select(x => new Application.Model.Common.News
                {
                    CreateTime = x.CreateTime,
                    NewsId = x.NewsId,
                    Title = x.Title
                }).ToList()
            };
        }

        public GetModuleNewsResponse GetModuleNews(GetModuleNewsRequest request)
        {
            var response = new GetModuleNewsResponse();
            if (request.ModuleId < 0)
                throw new TransactionException("01", "moduleId 非法");
            if (request.AppId <= 0)
                throw new TransactionException("01", "AppId不允许为空");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
            var result = newsDa.GetModuleNews(request.ModuleId, request.AppId, request.NewsCount);
            if (result != null && result.Count > 0)
            {
                response.NewsInfos = result?.Select(x => new Application.Model.Common.News()
                {
                    NewsId = x.NewsId,
                    Title = x.Title,
                    CreateTime = x.CreateTime,
                    ImageUrl = x.ImageUrl,
                    ExternalUrl = x.ExternalUrl,
                    IsExternal = x.IsExternal==1
                }).ToList();
                response.Code = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.Code = "96";
                response.Message = "查无数据";
            }
            return response;
        }

        public GetNewsDetailResponse GetNewsDetail(GetNewsDetailRequest request)
        {
            var response = new GetNewsDetailResponse();
            if (request.NewsId < 0)
                throw new TransactionException("01", "NewsId 非法");
            if (request.AppId <= 0)
                throw new TransactionException("01", "AppId不允许为空");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
            var result = newsDa.GetNewsDetail(request.NewsId, request.AppId);
            if (result != null)
            {
                response.NewsId = result.NewsId;
                response.Title = result.Title;
                response.HtmlContent = result.HtmlContent;
                response.Author = result.Author;
                response.Source = result.Source;
                response.Module = result.Module;
                response.CreateTime = result.CreateTime;
                response.TestId = result.TestId;
                response.IsAttach = result.IsAttach;
                response.Attach1 = result.Attach1;
                response.Attach2 = result.Attach2;
                response.Attach3 = result.Attach3;
                response.Attach4 = result.Attach4;
                response.ViewCount = result.ViewCount;
                response.CanStudy = result.CanStudy;
                response.Code = "00";
                response.Message = "查询成功";
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