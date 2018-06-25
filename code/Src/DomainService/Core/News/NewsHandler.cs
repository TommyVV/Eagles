using System;
using System.Linq;
using System.Collections.Generic;
using Eagles.Base;
using Eagles.Interface.Core.News;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.DataAccess.NewsDa;
using Eagles.Interface.DataAccess.UserArticle;
using Eagles.Application.Model.News.CompleteTest;
using Eagles.Application.Model.News.CreateNews;
using Eagles.Application.Model.News.GetModuleNews;
using Eagles.Application.Model.News.GetNews;
using Eagles.Application.Model.News.GetNewsDetail;
using Eagles.Application.Model.News.GetNewsTest;
using Eagles.DomainService.Model.User;

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
            //    response.Code = "00";
            //    response.Message = "成功";
            //}
            //else
            //{
            //    response.Code = "96";
            //    response.Message = "失败";
            //}
            return response;
        }

        public GetNewsResponse GetUserArticle(GetNewsRequest request)
        {
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
            if (request.AppId <= 0)
                throw new TransactionException("01", "appId 不允许为空");
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
            if (request.ModuleId < 0)
                throw new TransactionException("01", "moduleId 非法");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
            if (request.AppId <= 0)
                throw new TransactionException("01", "appId 不允许为空");
            var result = newsDa.GetModuleNews(request.ModuleId, request.AppId, request.NewsCount);
            if (result != null && result.Count > 0)
            {
                response.NewsInfos = result?.Select(x => new Application.Model.Common.News()
                {
                    NewsId = x.NewsId,
                    Title = x.Title,
                    CreateTime = x.CreateTime,
                    ImageUrl = x.ImageUrl,
                    ExternalUrl = x.ExternalUrl,
                    IsExternal = x.IsExternal==1
                }).ToList();
                response.Code = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.Code = "96";
                response.Message = "查无数据";
            }
            return response;
        }

        public GetNewsDetailResponse GetNewsDetail(GetNewsDetailRequest request)
        {
            var response = new GetNewsDetailResponse();
            if (request.NewsId < 0)
                throw new TransactionException("01", "NewsId 非法");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
            if (request.AppId <= 0)
                throw new TransactionException("01", "appId 不允许为空");
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
                response.Code = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.Code = "96";
                response.Message = "查无数据";
            }
            return response;
        }

        public GetNewsTestResponse GetNewsTest(GetNewsTestRequest request)
        {
            var response = new GetNewsTestResponse();
            if (request.TestId < 0)
                throw new TransactionException("01", "TestId 非法");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
            if (request.AppId <= 0)
                throw new TransactionException("01", "appId 不允许为空");
            var resultTest = newsDa.GetNewsTest(request.TestId);
            if (resultTest == null || !resultTest.Any())
            {
                return response;
            }
            //遍历问题
            var questions = new List<AppQuestion>();
            resultTest.ForEach(x =>
            {
                var answer = new AppAnswer()
                {
                    QuestionId = x.QuestionId,
                    AnswerId = x.AnswerId,
                    Answer = x.Answer,
                    AnswerType = x.AnswerType,
                    IsRight = x.IsRight,
                    ImageUrl = x.ImageUrl
                };
                var nowQuestion = questions.Find(y => y.QuestionId == x.QuestionId);
                if (nowQuestion == null)
                {
                    questions.Add(new AppQuestion()
                    {
                        QuestionId = x.QuestionId,
                        Question = x.Question,
                        Multiple = x.Multiple,
                        MultipleCount = x.MultipleCount,
                        AnswerList = new List<AppAnswer>()
                        {
                            answer
                        }
                    });
                }
                else
                {
                    nowQuestion.AnswerList.Add(answer);
                }
            });
            response.TestList = questions;
            return response;
        }

        public CompleteTestResponse CompleteTest(CompleteTestRequest request)
        {
            var response = new CompleteTestResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.Code = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var userInfo = util.GetUserInfo(tokens.UserId);
            if (userInfo == null)
            {
                throw new TransactionException("01", "用户不存在");
            }
            int testScore = 0; //答题分数
            //查询TB_TEST_PAPER
            var testPaper = newsDa.GetTestPaperInfo(request.TestId);
            var passScore = testPaper.PassScore; //及格分数
            var questionSocre = testPaper.QuestionSocre; //每题分值
            var passAwardScore = testPaper.PassAwardScore; //及格奖励积分

            //查询正确答案
            var rightAnswer = newsDa.GetTestRightAnswer(request.TestId);
            var answerList = request.TestList;
            //匹配正确答案
            int i = 0;
            answerList.ForEach(x =>
                {
                    var score = rightAnswer.Find(y => x.AnswerId == y.AnswerId);
                    if (score != null && 1 == score.IsRight)
                        i++;
                }
            );
            testScore = questionSocre * i; //每题分数*答对数量=答题分数

            //插入tb_user_test
            var userTest = new TbUserTest()
            {
                OrgId = tokens.OrgId,
                BranchId = tokens.BranchId,
                UserId = tokens.UserId,
                TestId = request.TestId,
                Score = testScore,
                TotalScore = 0,
                CreateTime = DateTime.Now,
                UseTime = request.UseTime
            };
            newsDa.CreateUserTest(userTest);
            
            //如果及格,给用户奖励积分
            if (testScore >= passScore)
            {

                //插入user_score_trace
                var userScoreTrace = new TbUserScoreTrace()
                {
                    UserId = tokens.UserId,
                    CreateTime = DateTime.Now,
                    Score = passAwardScore,
                    RewardsType = "50",
                    Comment = "完成试卷奖励积分",
                    OriScore = userInfo.Score,
                };
                util.CreateScoreLs(userScoreTrace);
                //修改用户积分
                util.EditUserScore(tokens.UserId, passAwardScore);
            }
            else
            {
                response.Code = "00";
                response.Message = "答题成功但未及格";
            }
            response.Code = "00";
            response.Message = "答题成功并奖励积分";
            return response;
        }
    }
}