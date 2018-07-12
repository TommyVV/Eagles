using Eagles.Base;
using Eagles.Application.Model.News.CreateNews;
using Eagles.Application.Model.News.GetNews;
using Eagles.Application.Model.News.GetNewsDetail;
using Eagles.Application.Model.News.GetModuleNews;
using Eagles.Application.Model.News.GetPublicNews;
using Eagles.Application.Model.News.AddNewsViewCount;

namespace Eagles.Interface.Core.News
{
    public interface INewsHandler : IInterfaceBase
    {
        CreateArticleResponse CreateArticle(CreateArticleRequest request);

        AddNewsViewCountResponse AddNewsViewCount(AddNewsCountRequest request);

        GetNewsResponse GetUserArticle(GetNewsRequest request);

        GetPublicNewsResponse GetPublicNews(GetPublicNewsRequest request);

        GetModuleNewsResponse GetModuleNews(GetModuleNewsRequest request);

        GetNewsDetailResponse GetNewsDetail(GetNewsDetailRequest request);
    }
}