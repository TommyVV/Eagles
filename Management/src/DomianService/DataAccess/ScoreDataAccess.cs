using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Eagles.Application.Model.ScoreSetUp.Requset;
using Eagles.Base.Cache;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.Score;
using Eagles.DomainService.Model.User;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{
    public class ScoreDataAccess: IScoreDataAccess
    {
        private readonly IDbManager dbManager;

        private readonly ICacheHelper cacheHelper;

        public ScoreDataAccess(IDbManager dbManager, ICacheHelper cacheHelper)
        {
            this.dbManager = dbManager;
            this.cacheHelper = cacheHelper;
        }

        public int EditScoreSetUp(TbRewardScore mod)
        {
            return dbManager.Excuted(@"UPDATE `eagles`.`tb_reward_score`
SET
`OrgId` = @OrgId,
`BranchId` = @BranchId,
`RewardType` = @RewardType,
`Score` = @Score,
`keyWord` = @keyWord,
`LearnTime` = @LearnTime,
`WordCount` = @WordCount
WHERE `RewardId` = @RewardId

", mod);
        }

        
        

        public int CreateScoreSetUp(TbRewardScore mod)
        {
            return dbManager.Excuted(@"INSERT INTO `eagles`.`tb_reward_score`
(`RewardId`,
`OrgId`,
`BranchId`,
`RewardType`,
`Score`,
`keyWord`,
`LearnTime`,
`WordCount`)
VALUES
(@RewardId,
@OrgId,
@BranchId,
@RewardType,
@Score,
@keyWord,
@LearnTime,
@WordCount);


", mod);
        }

        public int RemoveScoreSetUp(RemoveScoreSetUpRequset requset)
        {

            return dbManager.Excuted(@"DELETE FROM `eagles`.`tb_reward_score`

WHERE    RewardId=@RewardId;
", new { RewardId=requset.ScoreSetUpId });
        }

        public TbRewardScore GetScoreSetUpDetail(GetScoreSetUpDetailRequset requset)

        {

            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_reward_score`.`RewardId`,
    `tb_reward_score`.`OrgId`,
    `tb_reward_score`.`BranchId`,
    `tb_reward_score`.`RewardType`,
    `tb_reward_score`.`Score`,
    `tb_reward_score`.`keyWord`,
    `tb_reward_score`.`LearnTime`,
    `tb_reward_score`.`WordCount`
FROM `eagles`.`tb_reward_score`  
  where RewardId=@RewardId;
 ");
            dynamicParams.Add("RewardId", requset.ScoreSetUpId);

            return dbManager.QuerySingle<TbRewardScore>(sql.ToString(), dynamicParams);
        }

        public List<TbRewardScore> GetScoreSetUps(GetScoreSetUpRequset requset,out int totalCount)
        {

            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            if (requset.OperationType > 0)
            {
                parameter.Append(" and RewardType = @RewardType ");
                dynamicParams.Add("RewardType", requset.OperationType);
            }

            var token = cacheHelper.GetData<TbUserToken>(requset.Token);
            if (token.BranchId > 0)
            {
                parameter.Append(" and BranchId = @BranchId ");
                dynamicParams.Add("BranchId", token.BranchId);
            }

            if (token.OrgId > 0)
            {
                parameter.Append(" and OrgId = @OrgId ");
                dynamicParams.Add("OrgId", token.OrgId);
            }



            totalCount = 0;
            sql.AppendFormat(@" SELECT `tb_reward_score`.`RewardId`,
    `tb_reward_score`.`OrgId`,
    `tb_reward_score`.`BranchId`,
    `tb_reward_score`.`RewardType`,
    `tb_reward_score`.`Score`,
    `tb_reward_score`.`keyWord`,
    `tb_reward_score`.`LearnTime`,
    `tb_reward_score`.`WordCount`
FROM `eagles`.`tb_reward_score`  where 1=1  {0}  
 ", parameter);

            return dbManager.Query<TbRewardScore>(sql.ToString(), dynamicParams);

            
        }

        public List<TbUserScoreTrace> GetScoreTrace(int requsetUserId)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_user_score_trace`.`OrgId`,
    `tb_user_score_trace`.`UserId`,
    `tb_user_score_trace`.`TraceId`,
    `tb_user_score_trace`.`CreateTime`,
    `tb_user_score_trace`.`Score`,
    `tb_user_score_trace`.`RewardsType`,
    `tb_user_score_trace`.`Comment`,
    `tb_user_score_trace`.`OriScore`
FROM `eagles`.`tb_user_score_trace`
  where UserId=@UserId;
 ");
            dynamicParams.Add("UserId", requsetUserId);

            return dbManager.Query<TbUserScoreTrace>(sql.ToString(), dynamicParams);
        }
    }
}
