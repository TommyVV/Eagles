using System;
using System.Linq;
using Eagles.Application.Model.Common;
using Eagles.Interface.Core.News;
using Eagles.Application.Model.Curd.News.CreateNews;
using Eagles.Application.Model.Curd.News.GetNews;
using Eagles.Base.DataBase;
using Eagles.Interface.Core.DataBase.UserArticle;
using Eagles.Interface.DataAccess.Util;


namespace Eagles.DomainService.Core
{
    public class NewsHandler : INewsHandler
    {
        private readonly IArticleDataAccess articleData;

        private readonly IUtil util;

        public NewsHandler(IArticleDataAccess articleData, IUtil util)
        {
            this.articleData = articleData;
            this.util = util;
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
                NewsList = news.Select(x => new News
                {
                    NewsContent = x.HtmlContent,
                    NewsDate = x.CreateTime,
                    NewsId = x.NewsId,
                    NewsTitle = x.Title,
                    NewsType = x.NewsType
                }).ToList()
            };
        }
    }
}