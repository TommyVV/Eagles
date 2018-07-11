using System;
using System.Linq;
using System.Collections.Generic;
using Eagles.Base;
using Eagles.Interface.Core.News;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.DataAccess.NewsDa;
using Eagles.Interface.DataAccess.UserArticle;
using Eagles.Application.Model;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.News.AddNewsViewCount;
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
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var userInfo = util.GetUserInfo(tokens.UserId);
            if (userInfo == null)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
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
            if (result <= 0)
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }

        public GetNewsResponse GetUserArticle(GetNewsRequest request)
        {
            var response = new GetNewsResponse();
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var news = articleData.GetUserNewsList(tokens.UserId, request.PageIndex, request.PageSize);
            if (news != null && news.Count > 0)
            {
                response.NewsList = news.Select(x => new Application.Model.Common.News
                {
                    NewsId = x.NewsId,
                    Title = x.Title,
                    CreateTime = x.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")
                }).ToList();
            }
            else
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            return response;
        }

        public GetModuleNewsResponse GetModuleNews(GetModuleNewsRequest request)
        {
            var response = new GetModuleNewsResponse();
            if (request.ModuleId < 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var result = newsDa.GetModuleNews(request.ModuleId, request.AppId, request.PageIndex, request.PageSize);
            if (result != null && result.Count > 0)
            {
                response.NewsInfos = result?.Select(x => new Application.Model.Common.News()
                {
                    NewsId = x.NewsId,
                    Title = x.Title,
                    CreateTime = x.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    ImageUrl = x.ImageUrl,
                    ExternalUrl = x.ExternalUrl,
                    IsExternal = x.IsExternal==1,
                    NewsContent=x.ShortDesc
                }).ToList();
            }
            else
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            return response;
        }

        public GetNewsDetailResponse GetNewsDetail(GetNewsDetailRequest request)
        {
            var response = new GetNewsDetailResponse();
            if (request.NewsId < 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var result = newsDa.GetNewsDetail(request.NewsId, request.AppId);
            if (result != null)
            {
                response.NewsId = result.NewsId;
                response.ShortDesc = result.ShortDesc;
                response.Title = result.Title;
                response.HtmlContent = result.HtmlContent;
                response.Author = result.Author;
                response.Source = result.Source;
                response.Module = result.Module;
                response.CreateTime = result.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                response.TestId = result.TestId;
                response.IsAttach = result.IsAttach;
                response.Attach = new List<Attachment>
                {
                    new Attachment()
                    {
                        AttachmentDownloadUrl = result.Attach1,
                        AttachName = result.AttachName1
                    },
                    new Attachment()
                    {
                        AttachmentDownloadUrl = result.Attach2,
                        AttachName = result.AttachName2
                    },
                    new Attachment()
                    {
                        AttachmentDownloadUrl = result.Attach3,
                        AttachName = result.AttachName3
                    },
                    new Attachment()
                    {
                        AttachmentDownloadUrl = result.Attach4,
                        AttachName = result.AttachName4
                    },
                };
                response.ViewCount = result.ViewCount;
                response.CanStudy = result.CanStudy;
            }
            else
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }

        public AddNewsViewCountResponse AddNewsViewCount(AddNewsCountRequest request)
        {
            if (request.NewsId < 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var result = newsDa.AddNewsViewCount(request.NewsId);
            return new AddNewsViewCountResponse();
        }
    }
}