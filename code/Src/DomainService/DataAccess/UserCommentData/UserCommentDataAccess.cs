using System;
using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.Interface.DataAccess.UserComment;

namespace Ealges.DomianService.DataAccess.UserCommentData
{
    public class UserCommentDataAccess : IUserCommentAccess
    {
        private readonly IDbManager dbManager;

        public UserCommentDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public int EditUserComment(int orgId, int id, int userId, string content)
        {
            return dbManager.Excuted(@"insert into eagles.tb_user_comment(OrgId,Id,Content,Createtime,UserId,ReviewStatus) value (@OrgId,@Id,@Content,@Createtime,@UserId,@ReviewStatus)",
                new
                {
                    OrgId = orgId,
                    Id = id,
                    Content = content,
                    Createtime = DateTime.Now,
                    UserId = userId,
                    ReviewStatus = "-1"
                });
        }

        public List<Eagles.DomainService.Model.User.TbUserComment> GetUserComment(int id)
        {
            return dbManager.Query<Eagles.DomainService.Model.User.TbUserComment>(
                @"select Id,OrgId,MessageId,Content,CreateTime,UserId,ReviewUser,ReviewTime from eagles.tb_user_comment where Id = @Id",
                new { Id = id });
        }

        public int AuditUserComment(int id)
        {
            return dbManager.Excuted("update eagles.tb_user_comment set ReviewStatus = 0 where Id = @Id and ReviewStatus = -1 ", new {Id = id});
        }
    }
}