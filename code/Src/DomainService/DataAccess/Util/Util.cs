using System;
using System.Linq;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.User;
using Eagles.Interface.DataAccess.Util;

namespace Ealges.DomianService.DataAccess.Util
{
    public class Util:IUtil
    {
        private readonly IDbManager dbManager;

        public Util(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public UserToken GetUserId(string token,int tokenType)
        {
            var tokens=dbManager.Query<UserToken>(@" SELECT OrgId,
                BranchId,
                UserId,
                Token,
                CreateTime,
                ExpireTime,
                TokenType
            FROM eagles.tb_user_token where Token=@Token AND TokenType=@TokenType",
                new {Token = new string [1] {token}, TokenType = new int[1] { tokenType } });
            if (tokens != null && tokens.Any())
            {
                return tokens.FirstOrDefault();
            }
            return null;
        }
    }
}
