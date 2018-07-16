using System;
using System.Collections.Generic;
using System.Linq;
using Eagles.Application.Model;
using Eagles.Application.Model.Exercises.Model;
using Eagles.Application.Model.Exercises.Requset;
using Eagles.Application.Model.Exercises.Response;
using Eagles.Base;
using Eagles.Base.Configuration;
using Eagles.Base.DataBase;
using Eagles.Base.DataBase.Modle;
using Eagles.DomainService.Model.Exercises;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class TestPaperHandler : ITestPaperHandler
    {

        private readonly IDbManager dbManager;

        private readonly ITestPaperDataAccess dataAccess;

        public TestPaperHandler(ITestPaperDataAccess dataAccess, IDbManager dbManager)
        {
            this.dataAccess = dataAccess;
            this.dbManager = dbManager;
        }


        /// <summary>
        /// 得到 题目详细信息。 包含所以选项信息id
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        public GetSubjectDetailResponse GetSubjectDetail(GetSubjectDetailRequset requset)
        {

            var response = new GetSubjectDetailResponse();

            TbQuestion result = dataAccess.GetSubjectDetail(requset);

            var optionList = dataAccess.GetOptionList(new List<int>() { requset.QuestionId });


            if (result == null) throw new TransactionException("M01", "无业务数据");

            response.Info = new SubjectDetails
            {
                Answer = result.AnswerType,
                QuestionId = result.QuestionId,
                Question = result.Question,
                OrgId = result.OrgId,
                OptionList = optionList.Select(x => new Option
                {
                    QuestionId = x.QuestionId,
                    Img = x.ImageUrl,
                    AnswerType = x.AnswerType,
                    IsImg = string.IsNullOrWhiteSpace(x.ImageUrl),
                    IsRight = x.IsRight,
                    OptionId = x.AnswerId,
                    OptionName = x.Answer,
                }).ToList(),
                MultipleCount = result.MultipleCount,
                Multiple = result.Multiple,
                //  ExercisesId = result.
            };
            return response;
        }

        /// <summary>
        /// 删除题目   删除题目时 1.删除题目对应所以选项 2.删除题目  3.删除所有与题目相关的试卷
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        public bool RemoveSubject(RemoveSubjectRequset requset)
        {

            // 删除题目答案表中的信息 事务处理  删除题目时 删除选项 删除题目  删除和试卷关联的关系

            return dataAccess.RemoveExercisesSubjectRelationship(requset);

        }

        /// <summary>
        /// 编辑/新增 题目 返回题目唯一id
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        public int EditSubject(EditSubjectRequset requset)
        {



            TbQuestion info;

            if (requset.Info.QuestionId > 0)
            {
                info = new TbQuestion
                {
                    AnswerType = requset.Info.Answer,
                    Multiple = requset.Info.Multiple,
                    MultipleCount = requset.Info.MultipleCount,
                    OrgId = requset.Info.OrgId,
                    Question = requset.Info.Question,
                    QuestionId = requset.Info.QuestionId
                };


                //var optionList = dataAccess.GetOptionList( new List<int>{requset.Info.QuestionId});


                dataAccess.RemoveOption(requset.Option.Select(x => x.OptionId).ToList());

                ////todo 事务修改 批量修改选项信息
                dataAccess.CreateOption(requset.Option.Select(x =>
                   new TbQuestAnswer
                   {
                       Answer = x.OptionName,
                       AnswerType = x.AnswerType,
                       ImageUrl = x.IsImg ? x.Img : string.Empty,
                       IsRight = x.IsRight,
                       OrgId = requset.Info.OrgId,
                       QuestionId = requset.Info.QuestionId,
                       AnswerId = x.OptionId
                   }).ToList());

                int result = dataAccess.EditSubject(info);

                //if (requset.OptionId.Count > 0)
                //{
                //    dataAccess.UpdataOption(info.QuestionId, requset.OptionId);
                //}

                return result < 0 ? 0 : requset.Info.QuestionId;
            }
            else
            {
                info = new TbQuestion
                {
                    AnswerType = requset.Info.Answer,
                    Multiple = requset.Info.Multiple,
                    MultipleCount = requset.Info.MultipleCount,
                    OrgId = requset.Info.OrgId,
                    Question = requset.Info.Question,
                };

                var questionId = dataAccess.CreateSubject(info);

                dataAccess.CreateOption(requset.Option.Select(x =>
                    new TbQuestAnswer
                    {
                        Answer = x.OptionName,
                        AnswerType = x.AnswerType,
                        ImageUrl = x.IsImg ? x.Img : string.Empty,
                        IsRight = x.IsRight,
                        OrgId = requset.Info.OrgId,
                        QuestionId = requset.Info.QuestionId,
                        AnswerId = x.OptionId
                    }).ToList());
                ////todo 事务添加 批量新增选项信息
                //dataAccess.CreateOption(requset.Info.OptionList.Select(x => new TbQuestAnswer
                //{
                //    Answer = x.OptionName,
                //    AnswerType = x.AnswerType,
                //    ImageUrl = x.IsImg ? x.Img : string.Empty,
                //    IsRight = x.IsRight,
                //    OrgId = requset.Info.OrgId,
                //    QuestionId = questionId
                //}).ToList());

                return questionId;


            }
        }

        /// <summary>
        /// 编辑/新增 试卷       新增试卷时 新增 试卷 + 习题关系
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        public ResponseBase EditExercises(EditExercisesRequset requset)
        {

            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };

            var now = DateTime.Now;

            TbTestPaper info;

            if (requset.Info.ExercisesId > 0)
            {

                info = new TbTestPaper
                {
                    BranchId = requset.Info.BranchId,
                    EditTime = now,
                    HasLimitedTime = requset.Info.HasLimitedTime ? 1 : 0,
                    HasReward = requset.Info.IsScoreAward,
                    HtmlDescription = requset.Info.HtmlDescription,
                    LimitedTime = requset.Info.LimitedTime,
                    OperId = 0, //todo 登陆人员id
                    OrgId = requset.Info.OrgId,
                    PassAwardScore = requset.Info.PassAwardScore,
                    PassScore = requset.Info.PassScore,
                    QuestionSocre = requset.Info.SubjectScore,
                    Status = requset.Info.Status,
                    TestId = requset.Info.ExercisesId,
                    TestName = requset.Info.ExercisesName,
                    TestType = requset.Info.ExercisesType,


                };

                int result = dataAccess.EditExercises(info);




                List<TbTestQuestion> list = requset.Subject.Select(x => new TbTestQuestion
                {
                    OrgId = requset.Info.OrgId,
                    QuestionId = x,
                    TestId = requset.Info.ExercisesId
                }).ToList();

                dataAccess.RemoveExercisesRelationship(new RemoveExercisesRequset
                {
                    ExercisesId = requset.Info.ExercisesId
                });
                //创建题目试卷 关系
                dataAccess.CreateTestQuestionRelationship(list);

                if (result < 0)
                {
                    response.IsSuccess = false;
                }




                if (result < 0)
                {
                    response.IsSuccess = false;
                }

            }
            else
            {
                info = new TbTestPaper
                {
                    BranchId = requset.Info.BranchId,
                    CreateTime = now,
                    HasLimitedTime = requset.Info.HasLimitedTime ? 1 : 0,
                    HasReward = requset.Info.IsScoreAward,
                    HtmlDescription = requset.Info.HtmlDescription,
                    LimitedTime = requset.Info.LimitedTime,
                    OperId = 0, //todo 登陆人员id
                    OrgId = requset.Info.OrgId,
                    PassAwardScore = requset.Info.PassAwardScore,
                    PassScore = requset.Info.PassScore,
                    QuestionSocre = requset.Info.SubjectScore,
                    Status = requset.Info.Status,
                    TestName = requset.Info.ExercisesName,
                    TestType = requset.Info.ExercisesType,

                };

                //得到新增的试卷id
                int result = dataAccess.CreateExercises(info);

                List<TbTestQuestion> list = requset.Subject.Select(x => new TbTestQuestion
                {
                    OrgId = requset.Info.OrgId,
                    QuestionId = x,
                    TestId = result
                }).ToList();
                //创建题目试卷 关系
                dataAccess.CreateTestQuestionRelationship(list);

                if (result < 0)
                {
                    response.IsSuccess = false;
                }
            }

            return response;
        }

        /// <summary>
        /// 删除试卷  删除试卷时 删除试卷 + 习题 关系
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        public bool RemoveExercises(RemoveExercisesRequset requset)
        {

            //return dbManager.ExcutedByTransaction(new List<TransactionCommand>()
            //{
            //    new TransactionCommand()
            //    {
            //        CommandString = @"DELETE FROM `eagles`.`tb_test_paper`  WHERE TestId=@TestId ",
            //        Parameter = new {TestId = requset.ExercisesId}
            //    },
            //    new TransactionCommand()
            //    {
            //        CommandString = @"DELETE FROM `eagles`.`tb_test_question` WHERE TestId=@TestId;",
            //        Parameter = new {TestId = requset.ExercisesId}
            //    },

            //});
            // 删除试卷 题目关系
            return dataAccess.RemoveExercises(requset);
            //int result1 = dataAccess.RemoveTestQuestionRelationshipByTestId(requset.ExercisesId);

        }

        /// <summary>
        /// 获取试卷详情   获取试卷详情时 会连带习题信息一起返回 
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        public GetExercisesDetailResponse GetExercisesDetail(GetExercisesDetailRequset requset)
        {
            var response = new GetExercisesDetailResponse();
            TbTestPaper info = dataAccess.GetExercisesDetail(requset);

            if (info == null) throw new TransactionException("M01", "无业务数据");

            //得到试卷 + 习题的关系
            List<TbTestQuestion> list = dataAccess.GetTestQuestionRelationshipByTestId(requset.ExercisesId);

            //找到所有习题的详细信息
            var subjectList = dataAccess.GetSubjectListByQuestionId(list.Select(x => x.QuestionId).ToList());

            response.Info = new ExercisesDetails
            {
                ExercisesId = info.TestId,
                ExercisesName = info.TestName,
                IsScoreAward = info.HasReward,
                ExercisesType = info.TestType,
                PassScore = info.PassScore,
                SubjectScore = info.QuestionSocre,
                Status = info.Status,
                PassAwardScore = info.PassAwardScore,
                // Content = FormatContent(), //TODO 上传内容格式
                SubjectList = subjectList.Select(x => new Subject
                {
                    Question = x.Question,
                    QuestionId = x.QuestionId
                }).ToList(),
                LimitedTime = info.LimitedTime,
                HasLimitedTime = info.HasLimitedTime > 0,
                CreateTime = info.CreateTime.ToString("yyyy-MM-dd"),
                HtmlDescription = info.HtmlDescription,

            };
            return response;
        }

        /// <summary>
        /// 获取所有试卷信息
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        public GetExercisesResponse GetExercises(GetExercisesRequset requset)
        {
            var response = new GetExercisesResponse
            {
                TotalCount = 0,
                List = new List<Exercises>(),
            };
            var list = dataAccess.GetExercisesList(requset, out int toltalcount) ?? new List<TbTestPaper>();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");



            response.TotalCount = toltalcount;
            response.List = list.Select(x => new Exercises
            {
                ExercisesId = x.TestId,
                ExercisesName = x.TestName,
                IsScoreAward = x.HasReward,
                ExercisesType = x.TestType,
                PassScore = x.PassScore,
                SubjectScore = x.QuestionSocre,
                Status = x.Status,
                PassAwardScore = x.PassAwardScore,
                LimitedTime = x.LimitedTime,
                HasLimitedTime = x.HasLimitedTime > 0,
                CreateTime = x.CreateTime.ToString("yyyy-MM-dd"),
            }).ToList();

            return response;
        }

        /// <summary>
        /// 上传内容格式
        /// </summary>
        /// <returns></returns>
        public string FormatContent()
        {
            return "";
        }

        /// <summary>
        /// 返回 不重复的 习题信息
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        public GetRandomSubjectResponse GetRandomSubject(GetRandomSubjectRequset requset)
        {

            var response = new GetRandomSubjectResponse();
            //得到试卷 + 习题的关系
            List<TbTestQuestion> list = dataAccess.GetTestQuestionRelationshipByTestId(requset.TestId);

            //数据库计算 派出已关联的 
            List<TbQuestion> subjectList =
                dataAccess.GetRandomSubject(list.Select(x => x.QuestionId).ToList(), requset.RandomSubjectSum);

            response.SubjectList = subjectList.Select(f => new Subject
            {
                Question = f.Question,
                QuestionId = f.QuestionId
            }).ToList();

            return response;
        }

        public int EditOption(EditOptionRequset requset)
        {
            throw new NotImplementedException();
        }

        //public int EditOption(EditOptionRequset requset)
        //{
        //    if (requset.Info.OptionId > 0)
        //    {
        //        var result = dataAccess.EditOption(
        //            new TbQuestAnswer
        //            {
        //                Answer = requset.Info.OptionName,
        //                AnswerType = requset.Info.AnswerType,
        //                ImageUrl = requset.Info.IsImg ? requset.Info.Img : string.Empty,
        //                IsRight = requset.Info.IsRight,
        //                OrgId = requset.OrgId,
        //                QuestionId = requset.Info.QuestionId,
        //                AnswerId = requset.Info.OptionId
        //            }

        //        );
        //        return result < 0 ? 0 : requset.Info.OptionId;
        //    }
        //    else
        //    {
        //        var result = dataAccess.CreateOption(
        //            new TbQuestAnswer
        //            {
        //                Answer = requset.Info.OptionName,
        //                AnswerType = requset.Info.AnswerType,
        //                ImageUrl = requset.Info.IsImg ? requset.Info.Img : string.Empty,
        //                IsRight = requset.Info.IsRight,
        //                OrgId = requset.OrgId,
        //                QuestionId = requset.Info.QuestionId
        //            });

        //        return result;
        //    }


        //}

        public bool RemoveOption(RemoveOptionRequset requset)
        {
            return dataAccess.RemoveOption(requset) > 0;
        }

        public bool RemoveSubjectInfo(RemoveSubjectInfoRequset requset)
        {
            return dataAccess.RemoveTestQuestionRelationshipByQuestionId(requset.QuestionId);
        }

        public GetSubjectListResponse GetSubjectList(GetSubjectListRequset requset)
        {

            var response = new GetSubjectListResponse
            {
                TotalCount = 0,
                List = new List<Subject>(),
            };
            var list = dataAccess.GetSubjectList(requset, out int toltalcount) ?? new List<TbQuestion>();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");

            response.TotalCount = toltalcount;
            response.List = list.Select(x => new Subject
            {
                Question = x.Question,
                QuestionId = x.QuestionId,
                Answer = x.AnswerType,
                Multiple = x.Multiple,
                MultipleCount = x.MultipleCount,
                OrgId = x.OrgId
            }).ToList();

            return response;

        }
    }
}
