using Eagles.Base;
using Eagles.Application.Model.TestPaper.CompleteTest;
using Eagles.Application.Model.TestPaper.GetTestPaper;
using Eagles.Application.Model.TestPaper.GetIsJoinTest;

namespace Eagles.Interface.Core.UserTest
{
    public interface IUserTestHandler : IInterfaceBase
    {
        GetTestPaperResponse GetTestPaper(GetTestPaperRequest request);

        CompleteTestResponse CompleteTest(CompleteTestRequest request);

        GetIsJoinTestResponse GetIsJoinTest(GetIsJoinTestRequest request);
    }
}