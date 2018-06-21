using System.Linq;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.User;
using Eagles.Interface.DataAccess.Util;

namespace Ealges.DomianService.DataAccess.Util
{
    public class Util : IUtil
    {
        private readonly IDbManager dbManager;

        public Util(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public UserToken GetUserId(string token,int tokenType)
        {
            var tokens = dbManager.Query<UserToken>(
                @" SELECT OrgId,BranchId,UserId,Token,CreateTime,ExpireTime,TokenType FROM eagles.tb_user_token where Token=@Token AND TokenType=@TokenType",
                new {Token = token, TokenType = tokenType});
            if (tokens != null && tokens.Any())
            {
                return tokens.FirstOrDefault();
            }
            return null;
        }
        
        public Eagles.DomainService.Model.User.UserInfo GetUserInfo(int userId)
        {
            var user = dbManager.Query<Eagles.DomainService.Model.User.UserInfo>(@" select IsLeader,Score from eagles.tb_user_info where UserId = @UserId", new { UserId = userId });
            if (user != null && user.Any())
            {
                return user.FirstOrDefault();
            }
            return null;
        }

        public int EditUserScore(int userId, int score)
        {
            return dbManager.Excuted(@"update eagles.tb_user_info set Score = Score + @Score where UserId = @UserId", new {UserId = userId, Score = score});
        }
    }
}