using System;
using Eagles.Base;
using Eagles.Application.Model.Curd.News.GetNews;
using Eagles.Application.Model.Curd.News.CreateNews;

namespace Eagles.Interface.Core.News
{
    public interface INewsHandler : IInterfaceBase
    {
        CreateNewsResponse CreateNews(CreateNewsRequest request);
        GetNewsResponse GetNews(GetNewsRequest request);
    }
}