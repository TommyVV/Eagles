using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.Enums;
using Eagles.Application.Model.News.Model;
using Eagles.Application.Model.News.Requset;
using Eagles.Application.Model.News.Response;
using Eagles.Base;
using Eagles.Base.Json.Implement;
using Eagles.DomainService.Model.News;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class NewsHandler : INewsHandler
    {
        private readonly INewsDataAccess dataAccess;

        public NewsHandler(INewsDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public bool EditNews(EditNewRequset requset)
        {

            TbNews mod;

            if (requset.Info.NewsId > 0)
            {
                mod = new TbNews
                {
                    Attach1 = requset.Info.Attach1,
                    Attach2 = requset.Info.Attach2,
                    Attach3 = requset.Info.Attach3,
                    Attach4 = requset.Info.Attach4,
                    Attach5 = requset.Info.Attach5,
                    Author = requset.Info.Author,
                    BeginTime = requset.Info.StarTime,
                    EndTime = requset.Info.EndTime,
                    HtmlContent = requset.Info.Content,
                    CreateTime = requset.Info.CreateTime,
                    Module = requset.Info.ModuleId,
                    NewsId = requset.Info.NewsId,
                    ImageUrl = requset.Info.NewsImg,
                    Title = requset.Info.NewsName,
                    // NewsType=NewsType.
                    Source = requset.Info.Source,
                    TestId = requset.Info.TestId,
                    OrgId = requset.Info.OrgId,
                };

                return dataAccess.EditNews(mod) > 0;

            }
            else
            {
                mod = new TbNews
                {
                    Attach1 = requset.Info.Attach1,
                    Attach2 = requset.Info.Attach2,
                    Attach3 = requset.Info.Attach3,
                    Attach4 = requset.Info.Attach4,
                    Attach5 = requset.Info.Attach5,
                    Author = requset.Info.Author,
                    BeginTime = requset.Info.StarTime,
                    EndTime = requset.Info.EndTime,
                    HtmlContent = requset.Info.Content,
                    CreateTime = requset.Info.CreateTime,
                    Module = requset.Info.ModuleId,
                    //    NewsId = requset.DetailInfo.NewsId,
                    ImageUrl = requset.Info.NewsImg,
                    Title = requset.Info.NewsName,
                    // NewsType=NewsType.
                    Source = requset.Info.Source,
                    TestId = requset.Info.TestId,
                    OrgId = requset.Info.OrgId,
                };

                return dataAccess.CreateNews(mod) > 0;

            }
        }

        public bool RemoveNews(RemoveNewRequset requset)
        {

            return dataAccess.RemoveNews(requset) > 0;

        }

        public GetNewDetailResponse GetNewsDetail(GetNewDetailRequset requset)
        {
            var response = new GetNewDetailResponse();

            TbNews detail = dataAccess.GetNewsDetail(requset);

            if (detail == null) throw new TransactionException("01", "无业务数据");

            response.Info = new NewDetail
            {
                //AuditStatus = AuditStatus.审核通过,
                AuditStatus = AuditStatus.审核通过,
                Author = detail.Author,
                CreateTime = detail.CreateTime,
                NewsId = detail.NewsId,
                NewsImg = detail.ImageUrl,
                NewsName = detail.Title,
                // NewsType=NewsType.
                Source = detail.Source,

                Attach1 = detail.Attach1,
                Attach2 = detail.Attach2,
                Attach3 = detail.Attach3,
                Attach4 = detail.Attach4,
                Attach5 = detail.Attach5,
                StarTime = detail.CreateTime,
                EndTime = detail.EndTime,
                ModuleId = detail.Module,
                TestId = detail.TestId,
                Content = detail.HtmlContent,
                OrgId = detail.OrgId,
                // Category=detail.ViewCount
            };
            return response;
        }

        public GetNewResponse GetNews(GetNewRequset requset)
        {

            var response = new GetNewResponse
            {
                TotalCount = 0
            };
            List<TbNews> list = dataAccess.GetNewsList(requset) ?? new List<TbNews>();

            if (list.Count == 0) throw new Exception("无数据");

            response.List = list.Select(x => new New
            {
                AuditStatus = AuditStatus.审核通过,
                Author = x.Author,
                CreateTime = x.CreateTime,
                NewsId = x.NewsId,
                NewsImg = x.ImageUrl,
                NewsName = x.Title,
                // NewsType=NewsType.
                Source = x.Source,
                OrgId = x.OrgId
            }).ToList();
            return response;
        }
    }
}
