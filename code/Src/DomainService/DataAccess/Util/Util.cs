using System.Linq;
using System.Text;
using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.Application.Model.Common;
using Eagles.Interface.DataAccess.Util;
using Eagles.DomainService.Model.Org;
using Eagles.DomainService.Model.User;
using Eagles.DomainService.Model.RewardScore;

namespace Ealges.DomianService.DataAccess.Util
{
    public class Util : IUtil
    {
        private readonly IDbManager dbManager;

        public Util(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public int CreateUserNotice(TbUserNotice userNotice)
        {
            //增加用户通知
            return dbManager.Excuted(@"insert into `eagles`.`tb_user_notice`(`OrgId`,`NewsType`,`Title`,`UserId`,`Content`,`IsRead`,`FromUser`,`CreateTime`,`TargetUrl`)
VALUES (@OrgId,@NewsType,@Title,@UserId,@Content,@IsRead,@FromUser,@CreateTime,@TargetUrl) ;", userNotice);
        }

        public int CreateScoreLs(TbUserScoreTrace userScoreTrace)
        {
            //增加修改积分的流水
            return dbManager.Excuted(@"insert into `eagles`.`tb_user_score_trace` (`OrgId`,`UserId`,`TraceId`,`CreateTime`,`Score`,`RewardsType`,`Comment`,`OriScore`)
VALUES (@OrgId,@UserId,@TraceId,@CreateTime,@Score,@RewardsType,@Comment,@OriScore);", userScoreTrace);
        }

        public int EditUserScore(int userId, int score)
        {
            //修改用户积分            
            return dbManager.Excuted(@"update eagles.tb_user_info set Score = Score - @Score where UserId = @UserId and Score>=@Score", new {UserId = userId, Score = score});
        }

        public bool BatchEditUserScore(List<JoinPeople> userList, int score)
        {
            if (userList.Count <= 0)
                return false;
            var sql = new StringBuilder();
            foreach (var user in userList)
                sql.Append(string.Format("{0},", user.UserId));
            sql.Remove(sql.ToString().LastIndexOf(','), 1);
            dbManager.Excuted(@"update eagles.tb_user_info set Score = Score - @Score where UserId in (@UserId) and Score>=@Score", new { UserId = sql, Score = score });
            return true;
        }

        public bool CheckAppId(int appId)
        {
            var user = dbManager.Query<TbOrgInfo>(@" select OrgId, OrgName, Province, City, District, Address, CreateTime, EditTime, OperId, Logo from eagles.tb_org_info where OrgId = @OrgId", new { OrgId = appId });
            if (user != null && user.Any())
                return true;
            return false;
        }

        public TbRewardScore RewardScore(string rewardType)
        {
            //查询任务奖励积分
            return dbManager.QuerySingle<TbRewardScore>("select Score,LearnTime from eagles.tb_reward_score where RewardType = @RewardType ", new { RewardType = rewardType });
        }

        public TbUserToken GetUserId(string token, int tokenType)
        {
            var tokens = dbManager.Query<TbUserToken>(@" select OrgId,BranchId,UserId,Token,CreateTime,ExpireTime,TokenType FROM eagles.tb_user_token 
where Token=@Token AND TokenType=@TokenType", new { Token = token, TokenType = tokenType });
            if (tokens != null && tokens.Any())
                return tokens.FirstOrDefault();
            return null;
        }

        public TbUserInfo GetUserInfo(int userId)
        {
            var user = dbManager.Query<TbUserInfo>(@"select OrgId,BranchId,UserId,Name,IsLeader,Score from eagles.tb_user_info where UserId = @UserId", new { UserId = userId });
            if (user != null && user.Any())
                return user.FirstOrDefault();
            return null;
        }
    }
}