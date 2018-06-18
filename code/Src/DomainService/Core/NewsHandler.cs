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

        public ResponseBase EditNews(EditNewsRequset requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };

            TB_NEWS mod;

            var json = new JsonSerialize();
            var attach = json.Deserialize<Dictionary<string, string>>(requset.Info.Attach);

            if (requset.Info.NewsId > 0)
            {
                mod = new TB_NEWS
                {
                    Attach1 = attach["Attach1"],
                    Attach2 = attach["Attach2"],
                    Attach3 = attach["Attach3"],
                    Attach4 = attach["Attach4"],
                    Attach5 = attach["Attach5"],
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
                };

                int result = dataAccess.EditNews(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
            }
            else
            {
                mod = new TB_NEWS
                {
                    Attach1 = attach["Attach1"],
                    Attach2 = attach["Attach2"],
                    Attach3 = attach["Attach3"],
                    Attach4 = attach["Attach4"],
                    Attach5 = attach["Attach5"],
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
                };

                int result = dataAccess.CreateNews(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
            }

            return response;

        }

        public ResponseBase RemoveNews(RemoveNewsRequset requset)
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

        public GetNewsDetailResponse GetNewsDetail(GetNewsDetailRequset requset)
        {
            var response = new GetNewsDetailResponse
            {
                ErrorCode = "00",
                Message = "成功",
            };
            TB_NEWS detail = dataAccess.GetNewsDetail(requset);
            var json = new JsonSerialize();

            if (detail == null) throw new Exception("无数据");

            response.Info = new NewsDetail
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
                Attach = json.SerializeObject(new
                {
                    detail.Attach1,
                    detail.Attach2,
                    detail.Attach3,
                    detail.Attach4,
                    detail.Attach5
                }),
                StarTime = detail.CreateTime,
                EndTime = detail.EndTime,
                ModuleId = detail.Module,
                TestId = detail.TestId,
                Content = detail.HtmlContent,
                // Category=detail.ViewCount
            };
            return response;
        }

        public GetNewsResponse GetNews(GetNewsRequset requset)
        {

            var response = new GetNewsResponse
            {
                TotalCount = 0,
                ErrorCode = "00",
                Message = "成功",
            };
            List<TB_NEWS> list = dataAccess.GetNewsList(requset) ?? new List<TB_NEWS>();

            if (list.Count == 0) throw new Exception("无数据");

            response.List = list.Select(x => new Application.Model.News.Model.News
            {
                AuditStatus= AuditStatus.审核通过,
                Author=x.Author,
                CreateTime=x.CreateTime,
                NewsId=x.NewsId,
                NewsImg=x.ImageUrl,
                NewsName=x.Title,
               // NewsType=NewsType.
               Source=x.Source
            }).ToList();
            return response;
        }
    }
}
