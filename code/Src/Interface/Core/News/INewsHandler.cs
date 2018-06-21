using Eagles.Base;
using Eagles.Application.Model.AppModel.News.GetNews;
using Eagles.Application.Model.AppModel.News.CreateNews;
using Eagles.Application.Model.AppModel.News.GetModuleNews;
using Eagles.Application.Model.AppModel.News.GetNewsDetail;
using Eagles.Application.Model.AppModel.News.GetNewsTest;

namespace Eagles.Interface.Core.News
{
    public interface INewsHandler : IInterfaceBase
    {
        CreateNewsResponse CreateNews(CreateNewsRequest request);
        GetNewsResponse GetUserArticle(GetNewsRequest request);

        GetModuleNewsResponse GetModuleNews(GetModuleNewsRequest request);

        GetNewsDetailResponse GetNewsDetail(GetNewsDetailRequest request);

        GetNewsTestResponse GetNewsTest(GetNewsTestRequest request);
    }
}