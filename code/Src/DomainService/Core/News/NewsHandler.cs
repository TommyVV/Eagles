using System.Linq;
using Eagles.Base;
using Eagles.Interface.Core.News;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.DataAccess.NewsDa;
using Eagles.Interface.Core.DataBase.UserArticle;
using Eagles.Application.Model.AppModel.News.GetNews;
using Eagles.Application.Model.AppModel.News.CreateNews;
using Eagles.Application.Model.AppModel.News.GetNewsTest;
using Eagles.Application.Model.AppModel.News.GetModuleNews;
using Eagles.Application.Model.AppModel.News.GetNewsDetail;

namespace Eagles.DomainService.Core.News
{
    public class NewsHandler : INewsHandler
    {
        private readonly IArticleDataAccess articleData;

        private readonly INewsDa newsDa;

        private readonly IUtil util;

        public NewsHandler(IArticleDataAccess articleData, IUtil util, INewsDa newsDa)
        {
            this.articleData = articleData;
            this.util = util;
            this.newsDa = newsDa;
        }

        public CreateNewsResponse CreateNews(CreateNewsRequest request)
        {
            var response = new CreateNewsResponse();
            var token = request.Token;
            var newsType = request.NewsType;
            var newsTitle = request.NewsTitle;
            var content = request.NewsContent;
            var isPublic = request.IsPublic;
            //var result = dbManager.Excuted(@"insert into eagles.tb_user_news (UserId,Title,HtmlContent,NewsType,Status,CreateTime,OrgReview,BranchReview)", new object[] { "", newsTitle, content, newsType, "-1", DateTime.Now, isPublic });
            //if (result > 0)
            //{
            //    response.ErrorCode = "00";
            //    response.Message = "成功";
            //}
            //else
            //{
            //    response.ErrorCode = "96";
            //    response.Message = "失败";
            //}
            return response;
        }

        public GetNewsResponse GetUserArticle(GetNewsRequest request)
        {
            //check token 
            if (string.IsNullOrEmpty(request.Token))
            {
                //to do error 
            }
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                return null;// todo error
            }
            
            var news = articleData.GetUserNewsList(tokens.UserId);
            //convert news 
            return new GetNewsResponse()
            {
                NewsList = news.Select(x => new Application.Model.Common.News
                {
                    CreateTime = x.CreateTime,
                    NewsId = x.NewsId,
                    Title = x.Title
                }).ToList()
            };
        }

        public GetModuleNewsResponse GetModuleNews(GetModuleNewsRequest request)
        {
            var response = new GetModuleNewsResponse();
            if (request.AppId < 0)
            {
                throw new TransactionException("01","appid 非法");
            }
            if (request.ModuleId < 0)
            {
                throw new TransactionException("01", "moduleId 非法");
            }
            var result = newsDa.GetModuleNews(request.ModuleId, request.AppId, request.NewsCount);
            if (result != null && result.Count > 0)
            {
                response.NewsInfos = result?.Select(x => new Application.Model.Common.News()
                {
                    NewsId = x.NewsId,
                    Title = x.Title,
                    CreateTime = x.CreateTime,
                    ImageUrl = x.ImageUrl
                }).ToList();
                response.ErrorCode = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查无数据";
            }
            return response;
        }

        public GetNewsDetailResponse GetNewsDetail(GetNewsDetailRequest request)
        {
            var response = new GetNewsDetailResponse();
            if (request.AppId < 0)
            {
                throw new TransactionException("01", "appid 非法");
            }
            if (request.NewsId < 0)
            {
                throw new TransactionException("01", "NewsId 非法");
            }
            var result = newsDa.GetNewsDetail(request.NewsId, request.AppId);
            if (result != null)
            {
                response.NewsId = result.NewsId;
                response.Title = result.Title;
                response.HtmlContent = result.HtmlContent;
                response.Author = result.Author;
                response.Source = result.Source;
                response.Module = result.Module;
                response.CreateTime = result.CreateTime;
                response.TestId = result.TestId;
                response.IsAttach = result.IsAttach;
                response.Attach1 = result.Attach1;
                response.Attach2 = result.Attach2;
                response.Attach3 = result.Attach3;
                response.Attach4 = result.Attach4;
                response.ViewCount = result.ViewCount;
                response.CanStudy = result.CanStudy;
                response.ErrorCode = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查无数据";
            }
            return response;
        }

        public GetNewsTestResponse GetNewsTest(GetNewsTestRequest request)
        {
            var response = new GetNewsTestResponse();
            if (request.AppId < 0)
            {
                throw new TransactionException("01", "appid 非法");
            }
            if (request.TestId < 0)
            {
                throw new TransactionException("01", "TestId 非法");
            }
            var resultTest = newsDa.GetNewsTest(request.TestId);
            if (resultTest == null || !resultTest.Any())
            {
                return response;
            }
            //遍历问题
            var testQuestion = resultTest.Select(x => new AppQuestion
            {
                QuestionId = x.QuestionId,
                Question = x.Question,
                Multiple = x.Multiple,
                MultipleCount=x.MultipleCount
            }).ToList();
            if (testQuestion == null || testQuestion.Count < 0)
            {
                response.ErrorCode = "96";
                response.Message = "查无数据";
            }
            else
            {
                response.ErrorCode = "00";
                response.Message = "查询成功";
            }
            //遍历答案
            var testAnwser = resultTest.Select(x => new AppAnswer()
            {
                QuestionId = x.QuestionId,
                AnswerId = x.AnswerId,
                Answer = x.Answer,
                AnswerType = x.AnswerType,
                IsRight = x.IsRight,
                ImageUrl = x.ImageUrl
            }).ToList();
            //遍历问题匹配答案
            testQuestion.ForEach(x =>
            {
                var sub = testAnwser.FindAll(y => x.QuestionId == y.QuestionId).ToList();
                if (sub.Any())
                {
                    x.AnswerList = sub;
                }
            });
            response.TestList = testQuestion;
            return response;
        }
    }
}