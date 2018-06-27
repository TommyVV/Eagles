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

        public List<Eagles.DomainService.Model.User.TbUserComment> GetUserComment(string commentType, int id, int userId)
        {
            return dbManager.Query<Eagles.DomainService.Model.User.TbUserComment>(@"select MessageId,Id,OrgId,MessageId,Content,CreateTime,UserId,ReviewUser,ReviewTime from eagles.tb_user_comment 
where CommentType = @CommentType and Id = @Id and (ReviewStatus = 0 or UserId = @UserId) ", new { CommentType = commentType, Id = id, UserId = userId });
        }

        public int AuditUserComment(int messageId)
        {
            return dbManager.Excuted("update eagles.tb_user_comment set ReviewStatus = 0 where MessageId = @MessageId and ReviewStatus = -1 ", new { MessageId = messageId });
        }
    }
}