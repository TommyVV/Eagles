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
            return dbManager.Excuted(@"insert into eagles.tb_user_comment(OrgId,Id,Content,Createtime,UserId,UserName,ReviewStatus,CommentType) 
value (@OrgId,@Id,@Content,@Createtime,@UserId,@UserName,@ReviewStatus,@CommentType)", userComment);
        }

        public int RemoveUserComment(int messageId, int id)
        {
            return dbManager.Excuted("delete from tb_user_comment where MessageId = @MessageId and Id = @Id ", new {MessageId = messageId, Id = id});
        }

        public int AuditUserComment(TbUserComment userComment)
        {
            return dbManager.Excuted("update eagles.tb_user_comment set ReviewStatus = @reviewStatus, ReviewUser = @ReviewUser, ReviewTime = @ReviewTime where MessageId = @MessageId and ReviewStatus = -1 ", userComment);
        }

        public List<TbUserComment> GetUserComment(string commentType, int id, int userId, int commentStatus)
        {
            if (commentStatus == 0)
                return dbManager.Query<TbUserComment>(@"select MessageId,Id,OrgId,MessageId,Content,CreateTime,UserId,UserName,ReviewUser,ReviewTime,ReviewStatus from eagles.tb_user_comment 
where CommentType = @CommentType and Id = @Id ", new {CommentType = commentType, Id = id});
            else
                return dbManager.Query<TbUserComment>(@"select MessageId,Id,OrgId,MessageId,Content,CreateTime,UserId,UserName,ReviewUser,ReviewTime,ReviewStatus from eagles.tb_user_comment 
where CommentType = @CommentType and Id = @Id and ReviewStatus = 0 union 
select MessageId,Id,OrgId,MessageId,Content,CreateTime,UserId,UserName,ReviewUser,ReviewTime,ReviewStatus from eagles.tb_user_comment 
where CommentType = @CommentType and Id = @Id and UserId = @UserId and ReviewStatus = -1 ", new {CommentType = commentType, Id = id, UserId = userId});
        }
    }
}