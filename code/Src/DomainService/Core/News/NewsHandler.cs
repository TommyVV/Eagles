using System.Linq;
using Eagles.Application.Model.AppModel.News;
using Eagles.Application.Model.AppModel.News.CreateNews;
using Eagles.Application.Model.AppModel.News.GetNews;
using Eagles.Application.Model.News.Model;
using Eagles.Base;
using Eagles.Interface.Core.DataBase.UserArticle;
using Eagles.Interface.Core.News;
using Eagles.Interface.DataAccess.NewsDA;
using Eagles.Interface.DataAccess.Util;

namespace Eagles.DomainService.Core.News
{
    public class NewsHandler : INewsHandler
    {
        private readonly IArticleDataAccess articleData;

        private readonly INewsDA newsDa;

        private readonly IUtil util;

        public NewsHandler(IArticleDataAccess articleData, IUtil util, INewsDA newsDa)
        {
            this.articleData = articleData;
            this.util = util;
            this.newsDa = newsDa;
        }

        public CreateNewsResponse CreateNews(CreateNewsRequest request)
        {
            var response = new CreateNewsResponse();
            var token = request.Token;
            var newsType = request.NewsType;
            var newsTitle = request.NewsTitle;
            var content = request.NewsContent;
            var isPublic = request.IsPublic;
            //var result = dbManager.Excuted(@"insert into eagles.tb_user_news (UserId,Title,HtmlContent,NewsType,Status,CreateTime,OrgReview,BranchReview)", new object[] { "", newsTitle, content, newsType, "-1", DateTime.Now, isPublic });
            //if (result > 0)
            //{
            //    response.ErrorCode = "00";
            //    response.Message = "成功";
            //}
            //else
            //{
            //    response.ErrorCode = "96";
            //    response.Message = "失败";
            //}
            return response;
        }

        public GetNewsResponse GetUserArticle(GetNewsRequest request)
        {
            //check token 
            if (string.IsNullOrEmpty(request.Token))
            {
                //to do error 
            }
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
                    NewsContent = x.HtmlContent,
                    NewsDate = x.CreateTime,
                    NewsId = x.NewsId,
                    NewsTitle = x.Title,
                    NewsType = x.NewsType
                }).ToList()
            };
        }

        public GetModuleNewsResponse GetModuleNews(GetModuleNewsRequest request)
        {
            if (request.AppId < 0)
            {
                throw new TransactionException("01","appid 非法");
            }

            if (request.ModuleId < 0)
            {
                throw new TransactionException("01", "moduleId 非法");
            }
            var result=newsDa.GetModuleNews(request.ModuleId, request.AppId,request.NewsCount);
            return new GetModuleNewsResponse()
            {
                NewsInfos = result.Select(x=>new New
                {
                    NewsId = x.NewsId,
                    NewsImg = x.ImageUrl,
                    NewsName = x.Title,
                    CreateTime = x.CreateTime,
                    Source = x.Source,
                    UserName = x.Author,
                }).ToList()
            };
        }

        public GetNewsDetailResponse GetNewsDetail(GetNewsDetailRequest request)
        {
            if (request.AppId < 0)
            {
                throw new TransactionException("01", "appid 非法");
            }

            if (request.NewsId < 0)
            {
                throw new TransactionException("01", "NewsId 非法");
            }

            var result = newsDa.GetNewsDetail(request.NewsId, request.AppId);
            return new GetNewsDetailResponse()
            {
                NewsDetail = new NewDetail()
                {
                    Author = result.Author,
                    NewsId = result.NewsId,
                    NewsName = result.Title,
                    Content = result.HtmlContent,
                    CreateTime = result.CreateTime,
                    ModuleId = result.Module,
                    NewsImg = result.ImageUrl,
                    TestId = result.TestId,
                    //todo 
                }
            };
        }
    }
}