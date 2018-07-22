using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Eagles.Application.Model;
using Eagles.Application.Model.Publicity.Request;
using Eagles.Base.Cache;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.Activity;
using Eagles.DomainService.Model.User;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{
    public class PublicityDataAccess : IPublicityDataAccess
    {
        private readonly IDbManager dbManager;
        private readonly ICacheHelper cacheHelper;

        public PublicityDataAccess(IDbManager dbManager, ICacheHelper cacheHelper)
        {
            this.dbManager = dbManager;
            this.cacheHelper = cacheHelper;
        }

        public TbActivity GetPublicActivityDetail(GetPublicActivityDetailRequest requset)
        {

            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@"SELECT `tb_activity`.`OrgId`,
    `tb_activity`.`BranchId`,
    `tb_activity`.`ActivityId`,
    `tb_activity`.`ActivityName`,
    `tb_activity`.`HtmlContent`,
    `tb_activity`.`BeginTime`,
    `tb_activity`.`EndTime`,
    `tb_activity`.`FromUser`,
    `tb_activity`.`FromUserName`,
    `tb_activity`.`ActivityType`,
    `tb_activity`.`MaxCount`,
    `tb_activity`.`CanComment`,
    `tb_activity`.`TestId`,
    `tb_activity`.`MaxUser`,
    `tb_activity`.`Attach1`,
    `tb_activity`.`Attach2`,
    `tb_activity`.`Attach3`,
    `tb_activity`.`Attach4`,
    `tb_activity`.`ImageUrl`,
    `tb_activity`.`IsPublic`,
    `tb_activity`.`OrgReview`,
    `tb_activity`.`BranchReview`,
    `tb_activity`.`ToUserId`,
    `tb_activity`.`ToUserName`,
    `tb_activity`.`Status`,
    `tb_activity`.`CreateType`,
    `tb_activity`.`AttachName1`,
    `tb_activity`.`AttachName2`,
    `tb_activity`.`AttachName3`,
    `tb_activity`.`AttachName4`,
    `tb_activity`.`PublicTime`
FROM `eagles`.`tb_activity`   where ActivityId=@ActivityId;
 ");
            dynamicParams.Add("ActivityId", requset.ActivityId);

            return dbManager.QuerySingle<TbActivity>(sql.ToString(), dynamicParams);
        }

        public List<TbActivity> GetPublicActivity(RequestBase requset)
        {

            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();
            var token = cacheHelper.GetData<TbUserToken>(requset.Token);

            if (token.OrgId > 0)
            {
                parameter.Append(" and  OrgId = @OrgId ");
                dynamicParams.Add("OrgId", token.OrgId);
            }


            sql.AppendFormat(@" SELECT `tb_activity`.`OrgId`,
    `tb_activity`.`BranchId`,
    `tb_activity`.`ActivityId`,
    `tb_activity`.`ActivityName`,
    `tb_activity`.`HtmlContent`,
    `tb_activity`.`BeginTime`,
    `tb_activity`.`EndTime`,
    `tb_activity`.`FromUser`,
    `tb_activity`.`ActivityType`,
    `tb_activity`.`MaxCount`,
    `tb_activity`.`CanComment`,
    `tb_activity`.`TestId`,
    `tb_activity`.`MaxUser`,
    `tb_activity`.`Attach1`,
    `tb_activity`.`Attach2`,
    `tb_activity`.`Attach3`,
    `tb_activity`.`Attach4`,
    `tb_activity`.`AttachName1`,
    `tb_activity`.`AttachName2`,
    `tb_activity`.`AttachName3`,
    `tb_activity`.`AttachName4`,
    `tb_activity`.`ImageUrl`,
    `tb_activity`.`IsPublic`,
    `tb_activity`.`OrgReview`,
    `tb_activity`.`BranchReview`,
    `tb_activity`.`ToUserId`,
    `tb_activity`.`Status`
FROM `eagles`.`tb_activity`   where 1=1  {0}  
 ", parameter);

            return dbManager.Query<TbActivity>(sql.ToString(), dynamicParams);
        }

        public TbTask GetPublicTaskDetail(GetPublicTaskDetailRequest requset)
        {

            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_task`.`OrgId`,
    `tb_task`.`BranchId`,
    `tb_task`.`TaskId`,
    `tb_task`.`TaskName`,
    `tb_task`.`FromUser`,
    `tb_task`.`FromUserName`,
    `tb_task`.`TaskContent`,
    `tb_task`.`BeginTime`,
    `tb_task`.`EndTime`,
    `tb_task`.`Attach1`,
    `tb_task`.`Attach2`,
    `tb_task`.`Attach3`,
    `tb_task`.`Attach4`,
    `tb_task`.`CreateTime`,
    `tb_task`.`CanComment`,
    `tb_task`.`Status`,
    `tb_task`.`IsPublic`,
    `tb_task`.`OrgReview`,
    `tb_task`.`BranchReview`,
    `tb_task`.`CreateType`,
    `tb_task`.`AttachName1`,
    `tb_task`.`AttachName2`,
    `tb_task`.`AttachName3`,
    `tb_task`.`AttachName4`,
    `tb_task`.`PublicTime`
FROM `eagles`.`tb_task`   where TaskId=@TaskId;
 ");
            dynamicParams.Add("TaskId", requset.TaskId);

            return dbManager.QuerySingle<TbTask>(sql.ToString(), dynamicParams);
        }

        public List<TbTask> GetPublicTask(RequestBase requset)
        {

            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();
            var token = cacheHelper.GetData<TbUserToken>(requset.Token);

            if (token.OrgId > 0)
            {
                parameter.Append(" and  OrgId = @OrgId ");
                dynamicParams.Add("OrgId", token.OrgId);
            }


            sql.AppendFormat(@" SELECT `tb_task`.`OrgId`,
    `tb_task`.`BranchId`,
    `tb_task`.`TaskId`,
    `tb_task`.`TaskName`,
    `tb_task`.`FromUser`,
    `tb_task`.`FromUserName`,
    `tb_task`.`TaskContent`,
    `tb_task`.`BeginTime`,
    `tb_task`.`EndTime`,
    `tb_task`.`Attach1`,
    `tb_task`.`Attach2`,
    `tb_task`.`Attach3`,
    `tb_task`.`Attach4`,
    `tb_task`.`CreateTime`,
    `tb_task`.`CanComment`,
    `tb_task`.`Status`,
    `tb_task`.`IsPublic`,
    `tb_task`.`OrgReview`,
    `tb_task`.`BranchReview`,
    `tb_task`.`CreateType`,
    `tb_task`.`AttachName1`,
    `tb_task`.`AttachName2`,
    `tb_task`.`AttachName3`,
    `tb_task`.`AttachName4`,
    `tb_task`.`PublicTime`
FROM `eagles`.`tb_task`    where 1=1  {0}  
 ", parameter);

            return dbManager.Query<TbTask>(sql.ToString(), dynamicParams);
        }

        public List<ActivityUserCount> GetActivityUserCount()
        {

            var sql = new StringBuilder();
            sql.AppendFormat(
                @" SELECT ActivityId,count(ActivityId) count FROM eagles.tb_user_activity group by ActivityId
 ");

            return dbManager.Query<ActivityUserCount>(sql.ToString());
        }


        public int GetActivityUserCount(int activityId)
        {
            return dbManager.ExecuteScalar<int>("select count(*) from eagles.tb_user_activity where ActivityId = @ActivityId ", new { ActivityId = activityId });
        }

        public List<TbUserTaskStep> GetTaskStep(int requsetTaskId)
        {
            return dbManager.Query<TbUserTaskStep>(@"select a.OrgId,a.BranchId,a.TaskId,a.UserId,b.Name,a.StepId,a.StepName,a.CreateTime,a.Content,a.UpdateTime,a.Attach1, a.Attach2, 
a.Attach3, a.Attach4, a.AttachName1, a.AttachName2, a.AttachName3, a.AttachName4 from eagles.tb_user_task_step a join eagles.tb_user_info b on a.UserId = b.UserId where a.TaskId = @taskId",
                new { TaskId = requsetTaskId });
        }
    }
}
