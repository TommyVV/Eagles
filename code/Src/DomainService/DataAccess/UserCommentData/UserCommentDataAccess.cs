using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.User;
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

        public int EditUserComment(TbUserComment userComment)
        {
            return dbManager.Excuted(@"insert into eagles.tb_user_comment(OrgId,Id,Content,Createtime,UserId,ReviewStatus,CommentType) 
value (@OrgId,@Id,@Content,@Createtime,@UserId,@ReviewStatus,CommentType)", userComment);
        }

        public int AuditUserComment(int messageId, int reviewStatus)
        {
            return dbManager.Excuted("update eagles.tb_user_comment set ReviewStatus = @reviewStatus where MessageId = @MessageId and ReviewStatus = -1 ", new { MessageId = messageId, ReviewStatus= reviewStatus });
        }

        public List<TbUserComment> GetUserComment(string commentType, int id, int userId)
        {
            return dbManager.Query<TbUserComment>(@"select MessageId,Id,OrgId,MessageId,Content,CreateTime,UserId,ReviewUser,ReviewTime from eagles.tb_user_comment 
where CommentType = @CommentType and Id = @Id and (ReviewStatus = 0 or UserId = @UserId) ", new { CommentType = commentType, Id = id, UserId = userId });
        }

    }
}