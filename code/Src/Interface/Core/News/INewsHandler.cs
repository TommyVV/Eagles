using Eagles.Base;
using Eagles.Application.Model.News.CreateNews;
using Eagles.Application.Model.News.CompleteTest;
using Eagles.Application.Model.News.GetNews;
using Eagles.Application.Model.News.GetNewsDetail;
using Eagles.Application.Model.News.GetTestPaper;
using Eagles.Application.Model.News.GetModuleNews;

namespace Eagles.Interface.Core.News
{
    public interface INewsHandler : IInterfaceBase
    {
        CreateArticleResponse CreateArticle(CreateArticleRequest request);

        GetNewsResponse GetUserArticle(GetNewsRequest request);

        GetModuleNewsResponse GetModuleNews(GetModuleNewsRequest request);

        GetNewsDetailResponse GetNewsDetail(GetNewsDetailRequest request);

    }
}