using System;
using System.Linq;
using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.User;
using Eagles.Interface.Core.UserTest;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.DataAccess.UserTest;
using Eagles.Application.Model;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.Enums;
using Eagles.Application.Model.News.CompleteTest;
using Eagles.Application.Model.News.GetTestPaper;
using Eagles.Base.Json;
using Eagles.DomainService.Core.Utility;
using Eagles.DomainService.Model.Question;

namespace Eagles.DomainService.Core.UserTest
{
    public class UserTestHandler : IUserTestHandler
    {
        private readonly IUserTestDataAccess testDa;

        private readonly IUtil util;

        private readonly IJsonSerialize jsonSerialize;

        public UserTestHandler(IUserTestDataAccess testDa, IUtil util, IJsonSerialize jsonSerialize)
        {
            this.testDa = testDa;
            this.util = util;
            this.jsonSerialize = jsonSerialize;
        }

        public GetTestPaperResponse GetTestPaper(GetTestPaperRequest request)
        {
            var response = new GetTestPaperResponse();
            if (request.TestId < 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var resultTest = testDa.GetTestPaper(request.TestId);
            if (resultTest == null || !resultTest.Any())
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
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
            response.FullScore = resultTest[0].QuestionSocre * questions.Count;
            response.PassScore = resultTest[0].PassScore;
            response.PassAwardScore = resultTest[0].PassAwardScore;
            response.LimitedTime = resultTest[0].LimitedTime;
            response.TestList = questions;
            response.UserCount = resultTest[0].UserCount;
            return response;
        }

        public CompleteTestResponse CompleteTest(CompleteTestRequest request)
        {
            var response = new CompleteTestResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            }

            var userInfo = util.GetUserInfo(tokens.UserId);
            if (userInfo == null)
            {
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            }

            //查询用户是否已经回答过该试题
            var userTest = testDa.GetUserTest(request.TestId, userInfo.UserId);
            if (userTest != null)
            {
                throw new TransactionException(MessageCode.RepeatJoin, MessageKey.RepeatJoin);
            }

            int testScore = 0; //答题分数
            //查询TB_TEST_PAPER
            var testPaper = testDa.GetTestPaperInfo(request.TestId);
            var passScore = testPaper.PassScore; //及格分数
            var questionSocre = testPaper.QuestionSocre; //每题分值
            var passAwardScore = testPaper.PassAwardScore; //及格奖励积分
            var testType = testPaper.TestType;

            var userAnswer = jsonSerialize.SerializeObject(request.TestList);

            //增加参与人数
            testDa.UpdateTestPaperUserCount(testPaper);
            //记录答案选择人数
            WriteUserAnwser(request.TestList);
            //如果是投票，没有正确答案。
            if (testType == ExercisesType.投票)
            {
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
                    UseTime = request.UseTime,
                    Answer = userAnswer
                };
                testDa.CreateUserTest(userTest);
                if (testScore >= 0)
                {
                    WriteScoreLs(tokens.UserId, passAwardScore, userInfo.Score);
                }

                return response;
            }

            //查询正确答案
            var rightAnswer = testDa.GetTestRightAnswer(request.TestId, 0);

            if (rightAnswer == null || !rightAnswer.Any())
            {
                //如果没有找到正确答案 直接返回，不加分（数据异常）
                return response;
            }

            var errorCount = 0;
            var resutlAnswer = new List<ResAnswer>();
            foreach (var question in request.TestList)
            {
                //判断所有答案是否是正确答案
                for (var i = 0; i < question.Answers.Count; i++)
                {

                    var questRight = rightAnswer.Where(x => x.QuestionId == question.QuestionId).ToList();
                    if (!questRight.Exists(x => x.AnswerId == question.Answers[i].AnswerId))
                    {
                        errorCount++;
                        resutlAnswer.Add(new ResAnswer()
                        {
                            QuestionId = question.QuestionId,
                            IsRight = false
                        });
                        break;
                    }

                    //如果已经没有答案了，但是这道题还有正确答案，则该题回答错误
                    if (i == question.Answers.Count - 1)
                    {
                        if (i == questRight.Count - 1)
                        {
                            resutlAnswer.Add(new ResAnswer()
                            {
                                QuestionId = question.QuestionId,
                                IsRight = true
                            });
                        }
                        else
                        {
                            errorCount++;
                            resutlAnswer.Add(new ResAnswer()
                            {
                                QuestionId = question.QuestionId,
                                IsRight = false
                            });
                        }
                    }
                }
            }

            //正确数量=总题数-未答题数量-错误数量
            var questionCount = rightAnswer.DistinctBy(x => x.QuestionId).Count();
            var userQuestionCount = request.TestList.DistinctBy(x => x.QuestionId).Count();
            var rightCount = questionCount - (questionCount - userQuestionCount) - errorCount;
            testScore = questionSocre * rightCount; //每题分数*答对数量=答题分数
            var totalScore = questionCount * questionSocre;
            response.TestScore = testScore;
            response.Score = passAwardScore;
            response.UseTime = request.UseTime;
            response.TestList = resutlAnswer;
            //插入tb_user_test
            userTest = new TbUserTest()
            {
                OrgId = tokens.OrgId,
                BranchId = tokens.BranchId,
                UserId = tokens.UserId,
                TestId = request.TestId,
                Score = testScore,
                TotalScore = totalScore,
                CreateTime = DateTime.Now,
                UseTime = request.UseTime,
                Answer = userAnswer
            };
            testDa.CreateUserTest(userTest);
            //如果及格,给用户奖励积分
            if (testScore >= passScore)
            {
                WriteScoreLs(tokens.UserId, passAwardScore, userInfo.Score);
            }

            return response;
        }

        private void WriteUserAnwser(List<ReqAnswer> answers)
        {
            if (answers == null || !answers.Any())
            {
                return;
            }
            var list=new List<TbQuestAnswer>();
            foreach (var question in answers)
            {
                list.AddRange(question.Answers.Select(x => new TbQuestAnswer()
                {
                    AnswerId = x.AnswerId,
                }).ToList());
            }
            
            testDa.WriteAnswerCount(list);


        }

        private void WriteScoreLs(int userId, int passAwardScore, int score)
        {
            //插入用户流水表user_score_trace
            var userScoreTrace = new TbUserScoreTrace()
            {
                UserId = userId,
                CreateTime = DateTime.Now,
                Score = passAwardScore,
                RewardsType = "50",
                Comment = "完成试卷奖励积分",
                OriScore = score,
            };
            util.CreateScoreLs(userScoreTrace);
            //修改用户积分
            util.EditUserScore(userId, passAwardScore);
        }
    }
}