using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Organization.Model;
using Eagles.Application.Model.SystemMessage.Model;
using Eagles.Application.Model.SystemMessage.Requset;
using Eagles.Application.Model.SystemMessage.Response;
using Eagles.Base;
using Eagles.Base.Cache;
using Eagles.DomainService.Model.News;
using Eagles.DomainService.Model.Org;
using Eagles.DomainService.Model.User;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class SystemNewsHandler: ISystemNewsHandler
    {
        private readonly ISystemNewsDataAccess dataAccess;

        private readonly ICacheHelper cacheHelper;

        public SystemNewsHandler(ISystemNewsDataAccess dataAccess, ICacheHelper cacheHelper)
        {
            this.dataAccess = dataAccess;
            this.cacheHelper = cacheHelper;
        }

        public bool EditSystemNews(EditSystemNewsRequset requset)
        {
            TbSystemNews mod;
            var now = DateTime.Now;
            var token = cacheHelper.GetData<TbUserToken>(requset.Token);
            if (requset.Info.NewsId > 0)
            {
                mod = new TbSystemNews
                {
                    HtmlDesc = requset.Info.HtmlDesc,
                    NewsId = requset.Info.NewsId,
                   // NewsContent = requset.Info.NewsContent,
                    NewsName = requset.Info.NewsName,
                    NewsType = requset.Info.NewsType,
                    NoticeTime = Convert.ToDateTime(requset.Info.NoticeTime),
                    OperId = token.UserId,
                    RepeatTime = requset.Info.RepeatTime,
                    Status = requset.Info.Status
                };

                return dataAccess.EditSystemNews(mod) > 0;


            }
            else
            {
                mod = new TbSystemNews
                {
                    HtmlDesc = requset.Info.HtmlDesc,
                    NewsId = requset.Info.NewsId,
                    //NewsContent = requset.Info.NewsContent,
                    NewsName = requset.Info.NewsName,
                    NewsType = requset.Info.NewsType,
                    NoticeTime = Convert.ToDateTime(requset.Info.NoticeTime),
                    OperId = token.UserId,
                    RepeatTime = requset.Info.RepeatTime,
                    Status = requset.Info.Status
                };
                return dataAccess.CreateSystemNews(mod) > 0;


            }
        }

        public bool RemoveSystemNews(RemoveSystemNewsRequset requset)
        {
            return dataAccess.RemoveSystemNews(requset) > 0;
        }

        public GetSystemNewsResponse SystemNews(GetSystemNewsRequset requset)
        {
            var response = new GetSystemNewsResponse
            {
                TotalCount = 0,

            };
            List<TbSystemNews> list = dataAccess.SystemNews(requset) ?? new List<TbSystemNews>();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");

            response.List = list.Select(x => new SystemNews()
            {
                HtmlDesc = x.HtmlDesc,
                NewsId = x.NewsId,
                //NewsContent = x.NewsContent,
                NewsName = x.NewsName,
                NewsType = x.NewsType,
                NoticeTime = x.NoticeTime.ToString("yyyy-MM-dd"),
                RepeatTime = x.RepeatTime,
                Status = x.Status
            }).ToList();
            return response;
        }

        public GetSystemNewsDetailResponse GetSystemNewsDetail(GetSystemNewsDetailRequset requset)
        {
            var response = new GetSystemNewsDetailResponse
            {

            };
            TbSystemNews detail = dataAccess.GetSystemNewsDetail(requset);

            if (detail == null) throw new TransactionException("M01", "无业务数据");

            response.News = new SystemNews()
            {
                HtmlDesc = detail.HtmlDesc,
                NewsId = detail.NewsId,
                NewsName = detail.NewsName,
                NewsType = detail.NewsType,
                NoticeTime = detail.NoticeTime.ToString("yyyy-MM-dd"),
                RepeatTime = detail.RepeatTime,
                Status = detail.Status
            };
            return response;
            
        }
    }
}
