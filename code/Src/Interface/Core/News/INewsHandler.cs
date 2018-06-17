using System;
using Eagles.Application.Model.Curd.News;
using Eagles.Base;
using Eagles.Application.Model.Curd.News.GetNews;
using Eagles.Application.Model.Curd.News.CreateNews;

namespace Eagles.Interface.Core.News
{
    public interface INewsHandler : IInterfaceBase
    {
        CreateNewsResponse CreateNews(CreateNewsRequest request);
        GetNewsResponse GetUserArticle(GetNewsRequest request);

        GetModuleNewsResponse GetModuleNews(GetModuleNewsRequest request);
    }
}