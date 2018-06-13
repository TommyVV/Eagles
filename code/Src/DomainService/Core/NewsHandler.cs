using System;
using Eagles.Interface.Core.News;
using Eagles.Application.Model.Curd.News.CreateNews;
using Eagles.Application.Model.Curd.News.GetNews;

namespace Eagles.DomainService.Core
{
    public class NewsHandler : INewsHandler
    {
        public CreateNewsResponse CreateNews(CreateNewsRequest request)
        {
            throw new NotImplementedException();
        }

        public GetNewsResponse GetNews(GetNewsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}