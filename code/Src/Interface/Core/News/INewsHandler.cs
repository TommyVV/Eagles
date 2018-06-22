using Eagles.Base;
using Eagles.Application.Model.AppModel.News.GetNews;
using Eagles.Application.Model.AppModel.News.CreateNews;
using Eagles.Application.Model.AppModel.News.CompleteTest;
using Eagles.Application.Model.AppModel.News.GetNewsTest;
using Eagles.Application.Model.AppModel.News.GetModuleNews;
using Eagles.Application.Model.AppModel.News.GetNewsDetail;

namespace Eagles.Interface.Core.News
{
    public interface INewsHandler : IInterfaceBase
    {
        CreateNewsResponse CreateNews(CreateNewsRequest request);

        GetNewsResponse GetUserArticle(GetNewsRequest request);

        GetModuleNewsResponse GetModuleNews(GetModuleNewsRequest request);

        GetNewsDetailResponse GetNewsDetail(GetNewsDetailRequest request);

        GetNewsTestResponse GetNewsTest(GetNewsTestRequest request);

        CompleteTestResponse CompleteTest(CompleteTestRequest request);
    }
}