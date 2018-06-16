using System;
using Eagles.Base.DataBase;
using Eagles.Interface.Core.News;
using Eagles.Application.Model.Curd.News.CreateNews;
using Eagles.Application.Model.Curd.News.GetNews;
using DomainModel = Eagles.DomainService.Model;

namespace Eagles.DomainService.Core.News
{
    public class NewsHandler : INewsHandler
    {
        private readonly IDbManager dbManager;

        public CreateNewsResponse CreateNews(CreateNewsRequest request)
        {
            var response = new CreateNewsResponse();
            var token = request.Token;
            var newsType = request.NewsType;
            var newsTitle = request.NewsTitle;
            var content = request.NewsContent;
            var isPublic = request.IsPublic;
            var result = dbManager.Excuted(@"insert into eagles.tb_user_news (UserId,Title,HtmlContent,NewsType,Status,CreateTime,OrgReview,BranchReview)", new object[]{"",newsTitle,content, newsType, "-1", DateTime.Now,isPublic });
            if (result > 0)
            {
                response.ErrorCode = "00";
                response.Message = "成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "失败";
            }
            return response;
        }

        public GetNewsResponse GetNews(GetNewsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}