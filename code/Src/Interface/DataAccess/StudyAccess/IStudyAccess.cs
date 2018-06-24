using Eagles.Base;
using Eagles.DomainService.Model.User;

namespace Eagles.Interface.DataAccess.StudyAccess
{
    public interface IStudyAccess : IInterfaceBase
    {
        int EditStudyTime(bool type, TbUserStudyLog userStudy);
        
        TbUserStudyLog GetStudyTime(int userId, int newsId, int moduleId);
    }
}