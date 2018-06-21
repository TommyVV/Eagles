using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.ActivityTask.Requset;
using Eagles.Application.Model.ActivityTask.Response;
using Eagles.DomainService.Model.Activity;
using Eagles.Interface.Core;
using Eagles.Interface.Core.DataBase;

namespace Eagles.DomainService.Core
{
    public class ActivityTaskHandler: IActivityTaskHandler
    {
        private readonly IActivityTaskDataAccess dataAccess;

        public ActivityTaskHandler(IActivityTaskDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }
        public ResponseBase EditActivity(EditActivityTaskInfoRequset requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };

            TB_ACTIVITY mod;

            if (requset.Info.ActivityTaskId > 0)
            {
                mod = new TB_ACTIVITY
                {
                    ActivityName = requset.Info.ActivityTaskName,
                    //ActivityType = requset.Info.ActivityTaskType,
                    Attach1 = requset.Info.Attach1,
                    Attach2 = requset.Info.Attach2,
                    Attach3 = requset.Info.Attach3,
                    Attach4 = requset.Info.Attach4,
                    AttachType1 = "",
                    AttachType2 = "",
                    AttachType3 = "",
                    AttachType4 = "",
                    BeginTime = requset.Info.BeginTime,
                    ActivityId = requset.Info.ActivityTaskId,
                    BranchId = requset.BranchId,
                    BranchReview = "",
                    CanComment = requset.Info.IsComment,
                    EndTime = requset.Info.EndTime,
                    FromUser = 0,
                    HtmlContent = requset.Info.Content,
                    ImageUrl = requset.Info.ImageUrl,
                    IsPublic = requset.Info.IsPublic,
                    MaxCount = requset.Info.MaxPartakePeople,
                    MaxUser = requset.Info.MaxPartakePeople,
                    OrgId = requset.OrgId,
                    OrgReview = "",
                    Status = 0,
                    TestId = requset.Info.ExampleId,
                    ToUserId = 0
                };

                int result = dataAccess.EditActivity(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
            }
            else
            {
                mod = new TB_ACTIVITY
                {
                    ActivityName = requset.Info.ActivityTaskName,
                    //ActivityType = requset.Info.ActivityTaskType,
                    Attach1 = requset.Info.Attach1,
                    Attach2 = requset.Info.Attach2,
                    Attach3 = requset.Info.Attach3,
                    Attach4 = requset.Info.Attach4,
                    AttachType1 = "",
                    AttachType2 = "",
                    AttachType3 = "",
                    AttachType4 = "",
                    BeginTime = requset.Info.BeginTime,
                    ActivityId = requset.Info.ActivityTaskId,
                    BranchId = requset.BranchId,
                    BranchReview = "",
                    CanComment = requset.Info.IsComment,
                    EndTime = requset.Info.EndTime,
                    FromUser = 0,
                    HtmlContent = requset.Info.Content,
                    ImageUrl = requset.Info.ImageUrl,
                    IsPublic = requset.Info.IsPublic,
                    MaxCount = requset.Info.MaxPartakePeople,
                    MaxUser = requset.Info.MaxPartakePeople,
                    OrgId = requset.OrgId,
                    OrgReview = "",
                    Status = 0,
                    TestId = requset.Info.ExampleId,
                    ToUserId = 0

                    //TragetUrl = requset.Info.ColumnAddress,
                    //Priority = requset.Info.OrderBy,
                    //SmallImageUrl = requset.Info.ColumnIcon,
                    //ImageUrl = requset.Info.ColumnImg,
                    //IndexDisplay = requset.Info.IsSetTop,
                    //ModuleType = requset.Info.ModuleType,
                    //OrgId = requset.Info.OrgId,
                    //ModuleName = requset.Info.ColumnName,
                    //IndexPageCount = requset.Info.IndexPageCount
                };

                int result = dataAccess.CreateActivity(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
            }

            return response;
        }

        public ResponseBase RemoveActivity(RemoveActivityTaskRequset requset)
        {

            throw new NotImplementedException();
        }

        public GetActivityTaskDetailResponse GetActivityDetail(GetActivityTaskDetailRequset requset)
        {
            throw new NotImplementedException();
        }

        public GetActivityTaskResponse GetActivity(GetActivityTaskRequset requset)
        {
            throw new NotImplementedException();
        }
    }
}
