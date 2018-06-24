using System;
using System.Collections.Generic;
using System.Linq;
using Eagles.Application.Model;
using Eagles.Application.Model.Exercises.Model;
using Eagles.Application.Model.Exercises.Requset;
using Eagles.Application.Model.Exercises.Response;
using Eagles.Base.Configuration;
using Eagles.DomainService.Model.Exercises;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class TestPaperHandler : ITestPaperHandler
    {


        private readonly ITestPaperDataAccess dataAccess;

        public ExercisesHandler(IExercisesDataAccess dataAccess, IConfigurationManager configurationManager)
        {
            this.dataAccess = dataAccess;
            this.configurationManager = configurationManager;
        }


        /// <summary>
        /// 得到 题目详细信息。 包含所以选项信息id
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        public GetSubjectDetailResponse GetSubjectDetail(GetSubjectDetailRequset requset)
        {

            var response = new GetSubjectDetailResponse
            {
                ErrorCode = "00",
                Message = "成功",

            };

            TbQuestion result = dataAccess.GetSubjectDetail(requset);

            var optionList = dataAccess.GetOptionList(new List<int>() { requset.QuestionId });


            if (result == null) throw new Exception("无数据");

            response.Info = new SubjectDetails
            {
                Answer = result.AnswerType,
                QuestionId = result.QuestionId,
                Question = result.Question,
                OrgId = result.OrgId,
                OptionList = optionList.Select(x => new Option
                {
                    Img = x.ImageUrl,
                    IsCustom = x.AnswerType == 1,
                    IsImg = string.IsNullOrWhiteSpace(x.ImageUrl),
                    IsTrue = x.IsRight == 0,
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
        public ResponseBase RemoveSubject(RemoveSubjectRequset requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };
            //todo 删除题目答案表中的信息 事务处理  删除题目时 删除选项 删除题目  删除和试卷关联的关系
            int subjectResult = dataAccess.RemoveSubject(requset);
            int result1 = dataAccess.RemoveTestQuestionRelationshipByQuestionId(requset.QuestionId);
            int optionResult = dataAccess.RemoveOptionByQuestionId(requset.QuestionId);

            if (subjectResult < 0)
            {
                response.IsSuccess = false;
            }

            return response;
        }

        /// <summary>
        /// 编辑/新增 题目 返回题目唯一id
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        public ResponseBase EditSubject(EditSubjectRequset requset)
        {

            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };

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

                //todo 事务修改 批量修改选项信息
                int editResult = dataAccess.EditOption(requset.Info.OptionList.Select(x => new TbQuestAnswer
                {
                    Answer = x.OptionName,
                    AnswerType = x.IsCustom ? 1 : 0,
                    ImageUrl = x.IsImg ? x.Img : string.Empty,
                    IsRight = x.IsTrue ? 0 : 1,
                    OrgId = requset.Info.OrgId,
                    QuestionId = requset.Info.QuestionId,
                    AnswerId = x.OptionId
                }).ToList());

                int result = dataAccess.EditSubject(info);
                if (result < 0)
                {
                    response.IsSuccess = false;
                }
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

                //todo 事务添加 批量新增选项信息
                int createResult = dataAccess.CreateOption(requset.Info.OptionList.Select(x => new TbQuestAnswer
                {
                    Answer = x.OptionName,
                    AnswerType = x.IsCustom ? 1 : 0,
                    ImageUrl = x.IsImg ? x.Img : string.Empty,
                    IsRight = x.IsTrue ? 0 : 1,
                    OrgId = requset.Info.OrgId,
                    QuestionId = requset.Info.QuestionId
                }).ToList());

                int result = dataAccess.CreateSubject(info);

                if (result < 0)
                {
                    response.IsSuccess = false;
                }
            }
            return response;
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
                    HasReward = requset.Info.IsScoreAward ? 1 : 0,
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
                    CreateTime = requset.Info.CreateTime,
                    HasLimitedTime = requset.Info.HasLimitedTime ? 1 : 0,
                    HasReward = requset.Info.IsScoreAward ? 1 : 0,
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

                List<TbTestQuestion> list = requset.Info.SubjectList.Select(x => new TbTestQuestion
                {
                    OrgId = requset.Info.OrgId,
                    QuestionId = x.QuestionId,
                    TestId = result
                }).ToList();
                //创建题目试卷 关系
                int result1 = dataAccess.CreateTestQuestionRelationship(list);
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
        public ResponseBase RemoveExercises(RemoveExercisesRequset requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };
            //TODO 删除试卷 题目关系
            int result = dataAccess.RemoveExercises(requset);
            int result1 = dataAccess.RemoveTestQuestionRelationshipByTestId(requset.ExercisesId);

            if (result < 0)
            {
                response.IsSuccess = false;
            }

            return response;
        }

        /// <summary>
        /// 获取试卷详情   获取试卷详情时 会连带习题信息一起返回 
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        public GetExercisesDetailResponse GetExercisesDetail(GetExercisesDetailRequset requset)
        {
            var response = new GetExercisesDetailResponse
            {
                ErrorCode = "00",
                Message = "成功",
            };
            TbTestPaper info = dataAccess.GetExercisesDetail(requset);

            if (info == null) throw new Exception("无数据");

            //得到试卷 + 习题的关系
            List<TbTestQuestion> list = dataAccess.GetTestQuestionRelationshipByTestId(requset.ExercisesId);

            //找到所有习题的详细信息
            var subjectList = dataAccess.GetSubjectListByQuestionId(list.Select(x => x.QuestionId).ToList());

            response.Info = new ExercisesDetails
            {
                ExercisesId = info.TestId,
                ExercisesName = info.TestName,
                IsScoreAward = info.HasReward > 0,
                ExercisesType = info.TestType,
                PassScore = info.PassScore,
                SubjectScore = info.QuestionSocre,
                Status = info.Status,
                PassAwardScore = info.PassAwardScore,
                Content = FormatContent(), //TODO 上传内容格式
                SubjectList = subjectList.Select(x => new Subject
                {
                    Question = x.Question,
                    QuestionId = x.QuestionId
                }).ToList(),
                LimitedTime = info.LimitedTime,
                HasLimitedTime = info.HasLimitedTime > 0,
                CreateTime = info.CreateTime,
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
                ErrorCode = "00",
                List = new List<Exercises>(),
                Message = "成功",
            };
            var list = dataAccess.GetExercisesList(requset, out int toltalcount) ?? new List<TbTestPaper>();

            if (list.Count == 0) throw new Exception("无数据");



            response.TotalCount = toltalcount;
            response.List = list.Select(x => new Exercises
            {
                ExercisesId = x.TestId,
                ExercisesName = x.TestName,
                IsScoreAward = x.HasReward > 0,
                ExercisesType = x.TestType,
                PassScore = x.PassScore,
                SubjectScore = x.QuestionSocre,
                Status = x.Status,
                PassAwardScore = x.PassAwardScore,
                LimitedTime = x.LimitedTime,
                HasLimitedTime = x.HasLimitedTime > 0,
                CreateTime = x.CreateTime,
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

            var response = new GetRandomSubjectResponse
            {
                ErrorCode = "00",
                Message = "成功",
            };
            //得到试卷 + 习题的关系
            List<TbTestQuestion> list = dataAccess.GetTestQuestionRelationshipByTestId(requset.ExercisesId);

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
    }
}
