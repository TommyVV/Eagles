using System;
using Eagles.Application.Model.AppModel.News;
using Eagles.Base;
using Eagles.Application.Model.AppModel.News.GetNews;
using Eagles.Application.Model.AppModel.News.CreateNews;

namespace Eagles.Interface.Core.News
{
    public interface INewsHandler : IInterfaceBase
    {
        CreateNewsResponse CreateNews(CreateNewsRequest request);
        GetNewsResponse GetUserArticle(GetNewsRequest request);

        GetModuleNewsResponse GetModuleNews(GetModuleNewsRequest request);

        GetNewsDetailResponse GetNewsDetail(GetNewsDetailRequest request);
    }
}