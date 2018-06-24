using System;
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
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = 0;
            var studyInfo = new TbUserStudyLog();
            var resultInfo = iStudyAccess.GetStudyTime(tokens.UserId, request.NewsId, request.ModuleId);
            if (resultInfo == null)
            {
                //insert
                studyInfo.OrgId = tokens.OrgId;
                studyInfo.BranchId = tokens.BranchId;
                studyInfo.UserId = tokens.UserId;
                studyInfo.NewsId = request.NewsId;
                studyInfo.ModuleId = request.ModuleId;
                studyInfo.StudyId = request.StudyId;
                studyInfo.CreateTime = DateTime.Now;
                result = iStudyAccess.EditStudyTime(false, studyInfo);
            }
            else
            {
                //update
                studyInfo.UserId = tokens.UserId;
                studyInfo.StudyId = request.StudyId;
                result = iStudyAccess.EditStudyTime(true, studyInfo);
            }
            if (result > 0)
            {
                response.ErrorCode = "00";
                response.Message = "学习时间记录成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "学习时间记录失败";
            }
            return response;
        }

        public GetStudyTimeResponse GetStudyTime(GetStudyTimeRequest request)
        {
            var response = new GetStudyTimeResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            if (request.AppId <= 0)
            {
                throw new TransactionException("01", "appId 不允许为空");
            }
            var result = iStudyAccess.GetStudyTime(tokens.UserId, request.NewsId, request.ModuleId);
            if (result != null)
            {
                response.StudyTime = result.StudyId;
                response.ErrorCode = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查询失败";
            }
            return response;
        }
    }
}