using System;
using Eagles.Application.Model;
using Eagles.Base;
using Eagles.DomainService.Model.User;
using Eagles.Interface.Core.Study;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.DataAccess.StudyAccess;
using Eagles.Application.Model.Study.GetStudyTime;
using Eagles.Application.Model.Study.EditStudyTime;

namespace Eagles.DomainService.Core.Study
{
    public class StudyHandler : IStudyHandler
    {
        private readonly IStudyAccess iStudyAccess;
        private readonly IUtil util;

        public StudyHandler(IStudyAccess iStudyAccess, IUtil util)
        {
            this.iStudyAccess = iStudyAccess;
            this.util = util;
        }

        public EditStudyTimeResponse EditStudyTime(EditStudyTimeRequest request)
        {
            var response = new EditStudyTimeResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            }
            var oriScore = util.GetUserInfo(tokens.UserId).Score;
            var result = 0;
            var studyInfo = new TbUserStudyLog();
            var scoreInfo = util.RewardScore("4"); //学习时间奖励
            var score = scoreInfo.Score; //奖励积分
            var learnTime = scoreInfo.LearnTime; //满足学习时间
            var resultInfo = iStudyAccess.GetStudyTime(tokens.UserId, request.NewsId, request.ModuleId);
            if (resultInfo == null)
            {
                //insert
                studyInfo.OrgId = tokens.OrgId;
                studyInfo.BranchId = tokens.BranchId;
                studyInfo.UserId = tokens.UserId;
                studyInfo.NewsId = request.NewsId;
                studyInfo.ModuleId = request.ModuleId;
                studyInfo.StudyTime = request.StudyTime;
                studyInfo.CreateTime = DateTime.Now;
                result = iStudyAccess.EditStudyTime(false, studyInfo);
                if (request.StudyTime >= learnTime)
                {
                    util.EditUserScore(tokens.UserId, score);
                    var scoreLs = new TbUserScoreTrace() { OrgId = tokens.OrgId, UserId = tokens.UserId, CreateTime = DateTime.Now, Score = score, RewardsType = "", Comment = "学习获得积分",OriScore= oriScore };
                    util.CreateScoreLs(scoreLs);
                }
            }
            else
            {
                //update
                studyInfo.UserId = tokens.UserId;
                studyInfo.StudyTime = request.StudyTime;
                result = iStudyAccess.EditStudyTime(true, studyInfo);
                var origStudyTime = resultInfo.StudyTime; //原学习时间
                if (origStudyTime < learnTime && (origStudyTime + request.StudyTime >= learnTime)) //原学习时间<满足学习时间 && 原学习时间+本次学习时间>=满足学习时间
                {
                    util.EditUserScore(tokens.UserId, score);
                    var scoreLs = new TbUserScoreTrace() { OrgId = tokens.OrgId, UserId = tokens.UserId, CreateTime = DateTime.Now, Score = score, RewardsType = "", Comment = "学习获得积分", OriScore = oriScore };
                    util.CreateScoreLs(scoreLs);
                }
            }
           
            return response;
        }

        public GetStudyTimeResponse GetStudyTime(GetStudyTimeRequest request)
        {
            var response = new GetStudyTimeResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var result = iStudyAccess.GetStudyTime(tokens.UserId, request.NewsId, request.ModuleId);
            if (result != null)
                response.StudyTime = result.StudyTime;
            else
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            return response;
        }
    }
}