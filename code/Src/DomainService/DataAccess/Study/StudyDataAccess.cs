using Eagles.Base.DataBase;
using Eagles.DomainService.Model.User;
using Eagles.Interface.DataAccess.StudyAccess;

namespace Ealges.DomianService.DataAccess.Study
{
    public class StudyDataAccess: IStudyAccess
    {
        private readonly IDbManager dbManager;

        public StudyDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public int EditStudyTime(bool exists, TbUserStudyLog userStudy)
        {
            if (exists)
            {
                return dbManager.Excuted(@"update eagles.tb_user_study_log set StudyTime = StudyTime + @StudyTime where UserId = @UserId ", userStudy);
            }
            else
            {
                return dbManager.Excuted(@"insert into eagles.tb_user_study_log (OrgId,BranchId,UserId,NewsId,ModuleId,StudyTime,CreateTime) values 
(@OrgId,@BranchId,@UserId,@NewsId,@ModuleId,@StudyTime,@CreateTime)", userStudy);
            }
        }

        public TbUserStudyLog GetStudyTime(int userId, int newsId, int moduleId)
        {
            return dbManager.QuerySingle<TbUserStudyLog>(@"select StudyTime from eagles.tb_user_study_log where UserId = @UserId and NewsId = @NewsId and ModuleId = @ModuleId ",
                new {UserId = userId, NewsId = newsId, ModuleId = moduleId});
        }
    }
}