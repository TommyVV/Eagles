using Eagles.Base;
using Eagles.Application.Model.Study.GetStudyTime;
using Eagles.Application.Model.Study.EditStudyTime;

namespace Eagles.Interface.Core.Study
{
    public interface IStudyHandler : IInterfaceBase
    {
        EditStudyTimeResponse EditStudyTime(EditStudyTimeRequest request);

        GetStudyTimeResponse GetStudyTime(GetStudyTimeRequest request);
    }
}