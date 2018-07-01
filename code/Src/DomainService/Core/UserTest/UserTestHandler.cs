using System;
using System.Linq;
using System.Collections.Generic;
using System.Transactions;
using Eagles.Base;
using Eagles.DomainService.Model.User;
using Eagles.Interface.Core.UserTest;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.DataAccess.UserTest;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.News.CompleteTest;
using Eagles.Application.Model.News.GetTestPaper;

namespace Eagles.DomainService.Core.UserTest
{
    public class UserTestHandler : IUserTestHandler
    {
        private readonly IUserTestDataAccess testDa;

        private readonly IUtil util;

        public UserTestHandler(IUserTestDataAccess testDa, IUtil util)
        {
            this.testDa = testDa;
            this.util = util;
        }

        public GetTestPaperResponse GetTestPaper(GetTestPaperRequest request)
        {
            var response = new GetTestPaperResponse();
            if (request.TestId < 0)
                throw new Base.TransactionException("01", "TestId 非法");
            if (request.AppId <= 0)
                throw new Base.TransactionException("01", "AppId不允许为空");
            if (util.CheckAppId(request.AppId))
                throw new Base.TransactionException("01", "AppId不存在");
            var resultTest = testDa.GetTestPaper(request.TestId);
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
                throw new Base.TransactionException("01", "用户不存在");
            }
            //查询用户是否已经回答过该试题
            var userTest = testDa.GetUserTest(request.TestId, userInfo.UserId);
            if (userTest != null)
                throw new Base.TransactionException("01", "该用户已参与过此题");
            int testScore = 0; //答题分数
            //查询TB_TEST_PAPER
            var testPaper = testDa.GetTestPaperInfo(request.TestId);
            var passScore = testPaper.PassScore; //及格分数
            var questionSocre = testPaper.QuestionSocre; //每题分值
            var passAwardScore = testPaper.PassAwardScore; //及格奖励积分
            //查询正确答案
            var rightAnswer = testDa.GetTestRightAnswer(request.TestId, 0);
            var answerList = new List<ResAnswer>();
            answerList = request.TestList.Select(x => new ResAnswer()
            {
                QuestionId = x.QuestionId,
                AnswerId = x.AnswerId
                
            }).ToList();
            //匹配正确答案
            int i = 0;
            answerList.ForEach(x =>
                {
                    var score = rightAnswer.Find(y => x.AnswerId == y.AnswerId);
                    if (score != null && 0 == score.IsRight)
                    {
                        x.IsRight = 0;
                        i++;
                    }
                    else
                    {
                        x.IsRight = 1;
                    }
                }
            );
            testScore = questionSocre * i; //每题分数*答对数量=答题分数
            //插入tb_user_test
            userTest = new TbUserTest()
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
            testDa.CreateUserTest(userTest);
            response.TestScore = testScore;
            response.Score = passAwardScore;
            response.UseTime = request.UseTime;
            response.TestList = answerList;
            //如果及格,给用户奖励积分
            if (testScore >= passScore)
            {
                //插入用户流水表user_score_trace
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
            return response;
        }
    }
}