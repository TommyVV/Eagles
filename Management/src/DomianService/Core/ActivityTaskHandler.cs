using System.Collections.Generic;
using System.Linq;
using Eagles.Application.Model.ActivityTask.Model;
using Eagles.Application.Model.ActivityTask.Requset;
using Eagles.Application.Model.ActivityTask.Response;
using Eagles.Application.Model.Common;
using Eagles.Base;
using Eagles.Base.Cache;
using Eagles.Base.Utility;
using Eagles.DomainService.Model.Activity;
using Eagles.DomainService.Model.User;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class ActivityTaskHandler : IActivityTaskHandler
    {
        private readonly IActivityTaskDataAccess dataAccess;

        private readonly ICacheHelper cacheHelper;

        public ActivityTaskHandler(IActivityTaskDataAccess dataAccess, ICacheHelper cacheHelper)
        {
            this.dataAccess = dataAccess;
            this.cacheHelper = cacheHelper;
        }

        public bool EditActivity(EditActivityTaskInfoRequset requset)
        {

            TbActivity mod;
            var token = cacheHelper.GetData<TbUserToken>(requset.Token);
            if (requset.DetailInfo.ActivityTaskId > 0)
            {
                mod = new TbActivity
                {
                    ActivityName = requset.DetailInfo.ActivityTaskName,
                    //ActivityType = requset.DetailInfo.ActivityTaskType,
                    Attach1 = requset.DetailInfo.Attach1,
                    Attach2 = requset.DetailInfo.Attach2,
                    Attach3 = requset.DetailInfo.Attach3,
                    Attach4 = requset.DetailInfo.Attach4,
                    BeginTime = requset.DetailInfo.BeginTime,
                    ActivityId = requset.DetailInfo.ActivityTaskId,
                    BranchId = token.BranchId,
                    BranchReview = "",
                    CanComment = requset.DetailInfo.IsComment,
                    EndTime = requset.DetailInfo.EndTime.ConvertToDateTime(),
                    FromUser = 0,
                    HtmlContent = requset.DetailInfo.Content,
                    ImageUrl = requset.DetailInfo.ImageUrl,
                    IsPublic = requset.DetailInfo.IsPublic,
                    MaxCount = requset.DetailInfo.EverybodyPeople,
                    MaxUser = requset.DetailInfo.MaxPartakePeople,
                    AttachName1 = requset.DetailInfo.AttachName1,
                    AttachName2 = requset.DetailInfo.AttachName2,
                    AttachName3 = requset.DetailInfo.AttachName3,
                    AttachName4 = requset.DetailInfo.AttachName4,
                    OrgId = token.OrgId,
                    OrgReview = "",
                    Status = 0,
                    TestId = requset.DetailInfo.ExampleId,
                    ToUserId = 0,

                };

                var result = dataAccess.EditActivity(mod);

                return result > 0;
            }
            else
            {
                mod = new TbActivity
                {
                    ActivityName = requset.DetailInfo.ActivityTaskName,
                    ActivityType = requset.DetailInfo.ActivityTaskType,
                    Attach1 = requset.DetailInfo.Attach1,
                    Attach2 = requset.DetailInfo.Attach2,
                    Attach3 = requset.DetailInfo.Attach3,
                    Attach4 = requset.DetailInfo.Attach4,
                    AttachName1 = requset.DetailInfo.AttachName1,
                    AttachName2 = requset.DetailInfo.AttachName2,
                    AttachName3 = requset.DetailInfo.AttachName3,
                    AttachName4 = requset.DetailInfo.AttachName4,
                    BeginTime = requset.DetailInfo.BeginTime,
                    ActivityId = requset.DetailInfo.ActivityTaskId,
                    BranchId = token.BranchId,
                    BranchReview = "",
                    CanComment = requset.DetailInfo.IsComment,
                    EndTime = requset.DetailInfo.EndTime.ConvertToDateTime(),
                    FromUser = 0,
                    HtmlContent = requset.DetailInfo.Content,
                    ImageUrl = requset.DetailInfo.ImageUrl,
                    IsPublic = requset.DetailInfo.IsPublic,
                    MaxCount = requset.DetailInfo.EverybodyPeople,
                    MaxUser = requset.DetailInfo.MaxPartakePeople,
                    OrgId = token.OrgId,
                    OrgReview = "",
                    Status = 0,
                    TestId = requset.DetailInfo.ExampleId,
                    ToUserId = 0

                    //TragetUrl = requset.DetailInfo.ColumnAddress,
                    //Priority = requset.DetailInfo.OrderBy,
                    //SmallImageUrl = requset.DetailInfo.ColumnIcon,
                    //ImageUrl = requset.DetailInfo.ColumnImg,
                    //IndexDisplay = requset.DetailInfo.IsSetTop,
                    //ModuleType = requset.DetailInfo.ModuleType,
                    //OrgId = requset.DetailInfo.OrgId,
                    //ModuleName = requset.DetailInfo.ColumnName,
                    //IndexPageCount = requset.DetailInfo.IndexPageCount
                };

                var result = dataAccess.CreateActivity(mod);

                return result > 0;
            }

        }

        public bool RemoveActivity(RemoveActivityTaskRequset requset)
        {

            var result = dataAccess.RemoveActivity(requset);

            return result > 0;
        }

        public GetActivityTaskDetailResponse GetActivityDetail(GetActivityTaskDetailRequset requset)
        {
            var response = new GetActivityTaskDetailResponse
            {
            };
            var detail = dataAccess.GetActivityDetail(requset);

            if (detail == null) throw new TransactionException("M01", "无业务数据");
            var attac = new List<Attachment>()
            {
                new Attachment()
                {
                    AttachmentUrl = detail.Attach1,
                    AttachmentName = detail.AttachName1
                },
                new Attachment()
                {
                    AttachmentUrl = detail.Attach2,
                    AttachmentName = detail.AttachName2
                },
                new Attachment()
                {
                    AttachmentUrl = detail.Attach3,
                    AttachmentName = detail.AttachName3
                },new Attachment()
                {
                    AttachmentUrl = detail.Attach4,
                    AttachmentName = detail.AttachName4
                }
            };
            response.Info = new GetActivityDetail()
            {
                ActivityTaskName = detail.ActivityName,
                ActivityTaskType = detail.ActivityType,
                Attachments = attac,
                BeginTime = detail.BeginTime,
                ActivityTaskId = detail.ActivityId,
                IsComment = detail.CanComment,
                EndTime = detail.EndTime.FormartDatetime(),
                Content = detail.HtmlContent,
                ImageUrl = detail.ImageUrl,
                IsPublic = detail.IsPublic,
                EverybodyPeople = detail.MaxCount,
                MaxPartakePeople = detail.MaxUser,
                ExampleId = detail.TestId,
            };
            return response;
        }

        public GetActivityTaskResponse GetActivity(GetActivityTaskRequset requset)
        {
            var response = new GetActivityTaskResponse
            {
                TotalCount = 0,
            };
            List<TbActivity> list = dataAccess.GetGetActivityList(requset) ?? new List<TbActivity>();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");

            response.List = list.Select(x => new ActivityTaskModel
            {
                ActivityTaskName = x.ActivityName,
                ActivityTaskType = x.ActivityType,
                ActivityTaskId = x.ActivityId,
                ActivityTaskImg = x.ImageUrl,
            }).ToList();
            return response;
        }
    }
}
