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
using Eagles.Base.Json.Implement;
using Eagles.DomainService.Model.News;
using Eagles.Interface.Core;
using Eagles.Interface.Core.DataBase;

namespace Eagles.DomainService.Core
{
   public class NewsHandler: INewsHandler
    {
        private readonly INewsDataAccess dataAccess;

        public NewsHandler(INewsDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public ResponseBase EditNews(EditNewRequset requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };

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

                int result = dataAccess.EditNews(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
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
                //    NewsId = requset.Info.NewsId,
                    ImageUrl = requset.Info.NewsImg,
                    Title = requset.Info.NewsName,
                    // NewsType=NewsType.
                    Source = requset.Info.Source,
                    TestId = requset.Info.TestId,
                    OrgId = requset.Info.OrgId,
                };

                int result = dataAccess.CreateNews(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
            }

            return response;

        }

        public ResponseBase RemoveNews(RemoveNewRequset requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };
            int result = dataAccess.RemoveNews(requset);

            if (result > 0)
            {
                response.IsSuccess = true;
            }

            return response;
        }

        public GetNewDetailResponse GetNewsDetail(GetNewDetailRequset requset)
        {
            var response = new GetNewDetailResponse
            {
                ErrorCode = "00",
                Message = "成功",
            };
            TbNews detail = dataAccess.GetNewsDetail(requset);
            var json = new JsonSerialize();

            if (detail == null) throw new Exception("无数据");

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
                OrgId=detail.OrgId,
                // Category=detail.ViewCount
            };
            return response;
        }

        public GetNewResponse GetNews(GetNewRequset requset)
        {

            var response = new GetNewResponse
            {
                TotalCount = 0,
                ErrorCode = "00",
                Message = "成功",
            };
            List<TbNews> list = dataAccess.GetNewsList(requset) ?? new List<TbNews>();

            if (list.Count == 0) throw new Exception("无数据");

            response.List = list.Select(x => new Application.Model.News.Model.New
            {
                AuditStatus= AuditStatus.审核通过,
                Author=x.Author,
                CreateTime=x.CreateTime,
                NewsId=x.NewsId,
                NewsImg=x.ImageUrl,
                NewsName=x.Title,
               // NewsType=NewsType.
               Source=x.Source,
                OrgId=x.OrgId
            }).ToList();
            return response;
        }


    }
}
