using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.ActivityTask.Model;
using Eagles.Application.Model.ActivityTask.Requset;
using Eagles.Application.Model.ActivityTask.Response;
using Eagles.Base;
using Eagles.Base.Utility;
using Eagles.DomainService.Model.Activity;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class ActivityTaskHandler: IActivityTaskHandler
    {
        private readonly IActivityTaskDataAccess dataAccess;

        public ActivityTaskHandler(IActivityTaskDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public bool EditActivity(EditActivityTaskInfoRequset requset)
        {
           
            TbActivity mod;

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
                    BranchId = requset.BranchId,
                    BranchReview = "-1",
                    CanComment = requset.DetailInfo.IsComment,
                    EndTime = requset.DetailInfo.EndTime.ConvertToDateTime(),
                    FromUser = 0,
                    HtmlContent = requset.DetailInfo.Content,
                    ImageUrl = requset.DetailInfo.ImageUrl,
                    IsPublic = requset.DetailInfo.IsPublic,
                    MaxCount = requset.DetailInfo.MaxPartakePeople,
                    MaxUser = requset.DetailInfo.MaxPartakePeople,
                    OrgId = requset.OrgId,
                    OrgReview = "-1",
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
                    BeginTime = requset.DetailInfo.BeginTime,
                    ActivityId = requset.DetailInfo.ActivityTaskId,
                    BranchId = requset.BranchId,
                    BranchReview = "",
                    CanComment = requset.DetailInfo.IsComment,
                    EndTime = requset.DetailInfo.EndTime.ConvertToDateTime(),
                    FromUser = 0,
                    HtmlContent = requset.DetailInfo.Content,
                    ImageUrl = requset.DetailInfo.ImageUrl,
                    IsPublic = requset.DetailInfo.IsPublic,
                    MaxCount = requset.DetailInfo.EverybodyPeople,
                    MaxUser = requset.DetailInfo.MaxPartakePeople,
                    OrgId = requset.OrgId,
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
            TbActivity detail = dataAccess.GetActivityDetail(requset);

            if (detail == null) throw new TransactionException("M01","无业务数据");

            response.Info = new ActivityDetailInfo
            {
                ActivityTaskName = detail.ActivityName,
                ActivityTaskType = detail.ActivityType,
                Attach1 = detail.Attach1,
                Attach2 = detail.Attach2,
                Attach3 = detail.Attach3,
                Attach4 = detail.Attach4,
              
                BeginTime = detail.BeginTime,
                ActivityTaskId = detail.ActivityId,
                //BranchId = BranchId,
                //"" = BranchReview,
                IsComment = detail.CanComment,
                EndTime = detail.EndTime.FormartDatetime(),
               // 0 = FromUser,
                Content = detail.HtmlContent,
                ImageUrl = detail.ImageUrl,
                IsPublic = detail.IsPublic,
                EverybodyPeople = detail.MaxCount,
                MaxPartakePeople = detail.MaxUser,
                //requset.OrgId = OrgId,
               // "" = OrgReview,
              //  0 = detail.Status,
                ExampleId = detail.TestId,
               // 0                  ToUserId,

                //AuditStatus = AuditStatus.审核通过,
                //ColumnAddress = detail.TragetUrl,
                //ColumnId = detail.ModuleId,
                //ColumnName = detail.ModuleName,
                //OrderBy = detail.Priority,
                //ColumnIcon = detail.SmallImageUrl,
                //ColumnImg = detail.ImageUrl,
                //IsSetTop = detail.IndexDisplay,
                //ModuleType = detail.ModuleType,
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

            if (list.Count == 0) throw new TransactionException("M01","无业务数据");

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
