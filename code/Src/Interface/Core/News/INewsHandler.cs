using Eagles.Base;
using Eagles.Application.Model.News.CompleteTest;
using Eagles.Application.Model.News.CreateNews;
using Eagles.Application.Model.News.GetModuleNews;
using Eagles.Application.Model.News.GetNews;
using Eagles.Application.Model.News.GetNewsDetail;
using Eagles.Application.Model.News.GetTestPaper;

namespace Eagles.Interface.Core.News
{
    public interface INewsHandler : IInterfaceBase
    {
        CreateNewsResponse CreateNews(CreateNewsRequest request);

        GetNewsResponse GetUserArticle(GetNewsRequest request);

        GetModuleNewsResponse GetModuleNews(GetModuleNewsRequest request);

        GetNewsDetailResponse GetNewsDetail(GetNewsDetailRequest request);

        GetTestPaperResponse GetTestPaper(GetTestPaperRequest request);

        CompleteTestResponse CompleteTest(CompleteTestRequest request);
    }
}