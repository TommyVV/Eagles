using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.Enums;
using Eagles.Application.Model.News.Model;
using Eagles.Application.Model.News.Requset;
using Eagles.Application.Model.News.Response;
using Eagles.Base;
using Eagles.Base.Json.Implement;
using Eagles.Base.Utility;
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
            var now = DateTime.Now;
            if (requset.Info.NewsId > 0)
            {
                mod = new TbNews
                {
                    Attach1 = requset.Info.Attach1,
                    Attach2 = requset.Info.Attach2,
                    Attach3 = requset.Info.Attach3,
                    Attach4 = requset.Info.Attach4,
                    AttachName1 = requset.Info.AttachName1,
                    AttachName2 = requset.Info.AttachName2,
                    AttachName3 = requset.Info.AttachName3,
                    AttachName4 = requset.Info.AttachName4,
                    Author = requset.Info.Author,
                    BeginTime = requset.Info.StarTime,
                    EndTime = requset.Info.EndTime,
                    HtmlContent = requset.Info.Content,
                    //  CreateTime = requset.Info.CreateTime.ConvertToDateTime(),
                    Module = requset.Info.ModuleId,
                    NewsId = requset.Info.NewsId,
                    ImageUrl = requset.Info.NewsImg,
                    Title = requset.Info.NewsName,
                    NewsType = requset.Info.NewsType,
                    Source = requset.Info.Source,
                    TestId = requset.Info.TestId,
                    OrgId = requset.OrgId,
                    CanStudy = requset.Info.CanStudy,
                    ExternalUrl = requset.Info.ExternalUrl,
                    IsAttach = requset.Info.IsAttach,
                    IsClass = requset.Info.IsClass,
                    IsExternal = requset.Info.IsExternal,
                    IsImage = requset.Info.IsImage,
                    IsLearning = requset.Info.IsLearning,
                    IsText = requset.Info.IsLearning,
                    IsVideo = requset.Info.IsVideo,
                    OperId = 0,
                    ShortDesc = requset.Info.ShortDesc,
                    // Status=0,
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
                    AttachName1 = requset.Info.AttachName1,
                    AttachName2 = requset.Info.AttachName2,
                    AttachName3 = requset.Info.AttachName3,
                    AttachName4 = requset.Info.AttachName4,
                    Author = requset.Info.Author,
                    BeginTime = requset.Info.StarTime,
                    EndTime = requset.Info.EndTime,
                    HtmlContent = requset.Info.Content,
                    CreateTime = now,
                    Module = requset.Info.ModuleId,
                    NewsId = requset.Info.NewsId,
                    ImageUrl = requset.Info.NewsImg,
                    Title = requset.Info.NewsName,
                    NewsType = requset.Info.NewsType,
                    Source = requset.Info.Source,
                    TestId = requset.Info.TestId,
                    OrgId = requset.OrgId,
                    CanStudy = requset.Info.CanStudy,
                    ExternalUrl = requset.Info.ExternalUrl,
                    IsAttach = requset.Info.IsAttach,
                    IsClass = requset.Info.IsClass,
                    IsExternal = requset.Info.IsExternal,
                    IsImage = requset.Info.IsImage,
                    IsLearning = requset.Info.IsLearning,
                    IsText = requset.Info.IsLearning,
                    IsVideo = requset.Info.IsVideo,
                    OperId = 0,
                    ShortDesc = requset.Info.ShortDesc,
                    Status = "0",
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



            response.Info = new NewDetail
            {
                //AuditStatus = AuditStatus.审核通过,
                AuditStatus = AuditStatus.审核通过,
                Author = detail.Author,
                CreateTime = detail.CreateTime.FormartDatetime(),
                NewsId = detail.NewsId,
                NewsImg = detail.ImageUrl,
                NewsName = detail.Title,
                NewsType = detail.NewsType,
                Source = detail.Source,
                StarTime = detail.CreateTime,
                EndTime = detail.EndTime,
                ModuleId = detail.Module,
                TestId = detail.TestId,
                Content = detail.HtmlContent,
                //OrgId = detail.OrgId,
                ExternalUrl = detail.ExternalUrl,
                IsExternal = detail.IsExternal,
                AttachName1 = detail.AttachName1,
                AttachName2 = detail.AttachName2,
                AttachName3 = detail.AttachName3,
                AttachName4 = detail.AttachName4,
                Attach1 = detail.Attach1,
                Attach2 = detail.Attach2,
                Attach3 = detail.Attach3,
                Attach4 = detail.Attach4,

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
            List<TbNews> list = dataAccess.GetNewsList(requset, out int totalCount) ?? new List<TbNews>();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");
            response.TotalCount = totalCount;
            response.List = list.Select(x => new New
            {
                AuditStatus = AuditStatus.审核通过,
                Author = x.Author,
                CreateTime = x.CreateTime.FormartDatetime(),
                NewsId = x.NewsId,
                NewsImg = x.ImageUrl,
                NewsName = x.Title,
                // NewsType=NewsType.
                Source = x.Source,
                //OrgId = x.OrgId,

            }).ToList();
            return response;
        }

        public bool ImportNews(ImportNewRequset requset)
        {
            var now = DateTime.Now;

            List<TbNews> mod = requset.Info.Select(x => new TbNews
            {
                ImageUrl = x.NewsImg,
                Title = x.NewsName,
                ExternalUrl = x.ExternalUrl,
                IsExternal = 1,
                CreateTime = now
            }).ToList();

            return dataAccess.ImportNews(mod) > 0;
        }
    }
}
