using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.ActivityTask.Model;
using Eagles.Application.Model.Publicity.Model;
using Eagles.Application.Model.Publicity.Request;
using Eagles.Application.Model.Publicity.Response;
using Eagles.Base;
using Eagles.Base.Cache;
using Eagles.DomainService.Model.Activity;
using Eagles.DomainService.Model.User;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class PublicityHandler: IPublicityHandler
    {
        private readonly IPublicityDataAccess dataAccess;
        

        private readonly ICacheHelper cacheHelper;

        public PublicityHandler(ICacheHelper cacheHelper, IPublicityDataAccess dataAccess)
        {
            this.cacheHelper = cacheHelper;
            this.dataAccess = dataAccess;
        }

        public GetPublicActivityDetailResponse GetPublicActivityDetail(GetPublicActivityDetailRequest requset)
        {

            var response = new GetPublicActivityDetailResponse
            {

            };
            TbActivity detail = dataAccess.GetPublicActivityDetail(requset);

            if (detail == null) throw new TransactionException("M01", "无业务数据");

            response.Info = new PublicActivity
            {
                ActivityId = detail.ActivityId,
                ActivityName = detail.ActivityName,
                // ResponsibleUserName=detail.ToUserId,
                // UserCount=detail
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

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");

            response.Activitys = list.Select(x => new PublicActivity
            {
                ActivityId = x.ActivityId,
                ActivityName = x.ActivityName,
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

            response.info = new PublicTaskDetail()
            {
                //FromUser=detail.FromUser,
                TaskTitle=detail.TaskName,
                TaskId=detail.TaskId,
                TaskContent=detail.TaskContent,
                CreateTime=detail.CreateTime,
                
                // ResponsibleUserName=detail.ToUserId,
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
               CreateTime=x.CreateTime,
              // ResponsibleUserName=x.us
                TaskId=x.TaskId,
                TaskTitle=x.TaskName,
                
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
           //// TbTask detail = dataAccess.GetAritcleDetail(requset);

           // if (detail == null) throw new TransactionException("M01", "无业务数据");

           // response.info = new PublicTaskDetail()
           // {

           //     // ResponsibleUserName=detail.ToUserId,
           //     // UserCount=detail
           // };
            return response;
        }

        public GetPublicAritcleResponse GetPublicArticle(RequestBase requset)
        {
            throw new NotImplementedException();
        }
    }
}
