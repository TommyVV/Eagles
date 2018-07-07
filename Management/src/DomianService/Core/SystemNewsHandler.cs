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
using Eagles.DomainService.Model.News;
using Eagles.DomainService.Model.Org;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class SystemNewsHandler: ISystemNewsHandler
    {
        private readonly ISystemNewsDataAccess dataAccess;


        public SystemNewsHandler(ISystemNewsDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public bool EditSystemNews(EditSystemNewsRequset requset)
        {
            TbSystemNews mod;
            var now = DateTime.Now;
            if (requset.Info.NewsId > 0)
            {
                mod = new TbSystemNews
                {
                    HtmlDesc = requset.Info.HtmlDesc,
                    NewsId = requset.Info.NewsId,
                    NewsContent = requset.Info.NewsContent,
                    NewsName = requset.Info.NewsName,
                    NewsType = requset.Info.NewsType,
                    NoticeTime = requset.Info.NoticeTime,
                    OperId = requset.Info.OperId,
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
                    NewsContent = requset.Info.NewsContent,
                    NewsName = requset.Info.NewsName,
                    NewsType = requset.Info.NewsType,
                    NoticeTime = requset.Info.NoticeTime,
                    OperId = requset.Info.OperId,
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
                NewsContent = x.NewsContent,
                NewsName = x.NewsName,
                NewsType = x.NewsType,
                NoticeTime = x.NoticeTime,
                OperId = x.OperId,
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

            response.info = new SystemNews()
            {
                HtmlDesc = detail.HtmlDesc,
                NewsId = detail.NewsId,
                NewsContent = detail.NewsContent,
                NewsName = detail.NewsName,
                NewsType = detail.NewsType,
                NoticeTime = detail.NoticeTime,
                OperId = detail.OperId,
                RepeatTime = detail.RepeatTime,
                Status = detail.Status
            };
            return response;
            
        }
    }
}
