using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.ActivityTask.Model;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.News.Model;
using Eagles.Application.Model.News.Requset;
using Eagles.Application.Model.PartyMember.Requset;
using Eagles.Application.Model.Publicity.Model;
using Eagles.Application.Model.Publicity.Request;
using Eagles.Application.Model.Publicity.Response;
using Eagles.Base;
using Eagles.Base.Cache;
using Eagles.DomainService.Model.Activity;
using Eagles.DomainService.Model.News;
using Eagles.DomainService.Model.User;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;
using Step = Eagles.Application.Model.ActivityTask.Model.Step;

namespace Eagles.DomainService.Core
{
    public class PublicityHandler: IPublicityHandler
    {
        private readonly IPublicityDataAccess dataAccess;

        private readonly IPartyMemberDataAccess UserdataAccess;

        private readonly INewsDataAccess NewdataAccess;

        private readonly ICacheHelper cacheHelper;

        public PublicityHandler(ICacheHelper cacheHelper, IPublicityDataAccess dataAccess, IPartyMemberDataAccess userdataAccess, INewsDataAccess newdataAccess)
        {
            this.cacheHelper = cacheHelper;
            this.dataAccess = dataAccess;
            UserdataAccess = userdataAccess;
            NewdataAccess = newdataAccess;
        }

        public GetPublicActivityDetailResponse GetPublicActivityDetail(GetPublicActivityDetailRequest requset)
        {

            var response = new GetPublicActivityDetailResponse
            {

            };
            TbActivity detail = dataAccess.GetPublicActivityDetail(requset);

            if (detail == null) throw new TransactionException("M01", "无业务数据");

            int count = dataAccess.GetActivityUserCount(requset.ActivityId);

            response.Info = new PublicActivity
            {
                ActivityId = detail.ActivityId,
                ActivityName = detail.ActivityName,
                ResponsibleUserName =
                    UserdataAccess.GetUserInfoDetail(new GetUserInfoDetailRequest {UserId = detail.ToUserId}).Name,
                UserCount = count
            };
            return response;

        }

        public GetPublicActivityResponse GetPublicActivity(RequestBase requset)
        {

            var response = new GetPublicActivityResponse
            {
                TotalCount = 0,
            };
            List<TbActivity> list = dataAccess.GetPublicActivity(requset) ?? new List<TbActivity>();

            List<ActivityUserCount> count = dataAccess.GetActivityUserCount();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");

            var userInfo = UserdataAccess.GetUserInfoList(list.Select(x => x.FromUser).ToList()
                .Union(list.Select(x => x.ToUserId).ToList()).ToList());

            response.Activitys = list.Select(x => new PublicActivity
            {
                ActivityId = x.ActivityId,
                ActivityName = x.ActivityName,
                ResponsibleUserName = userInfo.FirstOrDefault(f => f.UserId == x.ToUserId)?.Name,
                CreateTime = x.PublicTime,
                UserCount = count.Any(d => d.ActivityId == x.ActivityId)
                    ? count.First(d => d.ActivityId == x.ActivityId).Count
                    : 0

            }).ToList();
            return response;
        }

        public GetPublicTaskDetailResponse GetPublicTaskDetail(GetPublicTaskDetailRequest requset)
        {
            var response = new GetPublicTaskDetailResponse
            {

            };
            TbTask detail = dataAccess.GetPublicTaskDetail(requset);

            if (detail == null) throw new TransactionException("M01", "无业务数据");

            List<TbUserTaskStep> result = dataAccess.GetTaskStep(requset.TaskId);

            response.info = new PublicTaskDetail()
            {
                //FromUser=detail.FromUser,
                TaskTitle = detail.TaskName,
                TaskId = detail.TaskId,
                TaskContent = detail.TaskContent,
                CreateTime = detail.CreateTime,
                Attachments = new List<Attachment>()
                {
                    new Attachment()
                    {
                        AttachmentName = detail.AttachName2,
                        AttachmentUrl = detail.Attach2
                    },
                    new Attachment()
                    {
                        AttachmentName = detail.AttachName3,
                        AttachmentUrl = detail.Attach3
                    },
                    new Attachment()
                    {
                        AttachmentName = detail.AttachName1,
                        AttachmentUrl = detail.Attach1
                    },
                    new Attachment()
                    {
                        AttachmentName = detail.AttachName4,
                        AttachmentUrl = detail.Attach4
                    }
                },
                ResponsibleUserName = detail.FromUserName,
                Steps = result?.Select(x => new Step
                {
                    StepId = x.StepId,
                    StepName = x.StepName,
                    StepContent = x.Content,

                }).ToList()
                // UserCount=detail
            };
            return response;

        }

        public GetPublicTaskResponse GetPublicTask(RequestBase requset)
        {

            var response = new GetPublicTaskResponse
            {
                TotalCount = 0,
            };
            List<TbTask> list = dataAccess.GetPublicTask(requset) ?? new List<TbTask>();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");

            response.Tasks = list.Select(x => new PublicTask
            {
                CreateTime = x.CreateTime,
                // ResponsibleUserName=x.us
                TaskId = x.TaskId,
                TaskTitle = x.TaskName,
                ResponsibleUserName = x.FromUserName
            }).ToList();
            return response;
        }

        public GetAritcleDetailResponse GetAritcleDetail(GetPublicArticleDetailRequest requset)
        {

            var response = new GetAritcleDetailResponse
            {
                //  CreateTime = x.CreateTime,
                // ResponsibleUserName=x.us


            };

            TbNews detail = NewdataAccess.GetNewsDetail(new GetNewDetailRequset()
            {
                NewsId = requset.NewsId
            });

            response.info = new AritcleDetail
            {
                Author = detail.Author,
                CreateTime = detail.CreateTime,
                NewsId = detail.NewsId,
                NewsDetail = detail.HtmlContent,
                NewsTitle = detail.Title

                // Category=detail.ViewCount
            };
            return response;
        }

        public GetPublicAritcleResponse GetPublicArticle(RequestBase requset)
        {


            var response = new GetPublicAritcleResponse
            {

            };
            List<TbNews> list = NewdataAccess.GetNewsList(requset.Token) ?? new List<TbNews>();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");


            response.Aritcles = list.Select(x => new Aritcle
            {
                CreateTime = x.CreateTime,
                NewsId = x.NewsId,
                NewsType = x.NewsType,
                NewsTitle = x.Title
            }).ToList();
            return response;

        }
    }
}
