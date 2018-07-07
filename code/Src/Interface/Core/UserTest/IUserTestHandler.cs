using Eagles.Base;
using Eagles.Application.Model.News.CompleteTest;
using Eagles.Application.Model.News.GetTestPaper;

namespace Eagles.Interface.Core.UserTest
{
    public interface IUserTestHandler : IInterfaceBase
    {
        GetTestPaperResponse GetTestPaper(GetTestPaperRequest request);

        CompleteTestResponse CompleteTest(CompleteTestRequest request);
    }
}