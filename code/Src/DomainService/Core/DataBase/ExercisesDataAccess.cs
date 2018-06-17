using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Eagles.Application.Model.enums;
using Eagles.Application.Model.Exercises.Model;
using Eagles.Application.Model.Exercises.Requset;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.Exercises;
using Eagles.DomainService.Model.TB_TEST;
using Eagles.Interface.Core.DataBase;

namespace Eagles.DomainService.Core.DataBase
{

    public class ExercisesDataAccess : IExercisesDataAccess
    {
        private readonly IDbManager dbManager;

        public List<TB_TEST_PAPER> GetExercisesList(GetExercisesRequset requset, out int totalCount)
        {

            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            if (requset.OrgId > 0)
            {
                parameter.Append(" and  OrgId = @OrgId ");
                dynamicParams.Add("OrgId", requset.OrgId);
            }

            if (requset.BranchId > 0)
            {
                parameter.Append(" and BranchId = @BranchId ");
                dynamicParams.Add("BranchId", requset.BranchId);
            }

            if (string.IsNullOrWhiteSpace(requset.ExercisesName))
            {
                parameter.Append(" and TestName = @TestName ");
                dynamicParams.Add("TestName", requset.ExercisesName);
            }

            if (requset.ExercisesType > 0)
            {
                parameter.Append(" and TestType = @TestType ");
                dynamicParams.Add("TestType", (int)requset.ExercisesType);
            }


            if (requset.Status > 0)
            {
                parameter.Append(" and Status = @Status ");
                dynamicParams.Add("Status", (int)requset.Status);
            }


            if (requset.StartTime != null)
            {
                parameter.Append(" and CreateTime <= @StartTime ");
                dynamicParams.Add("StartTime", requset.StartTime);
            }

            if (requset.EndTime != null)
            {
                parameter.Append(" and CreateTime >= @EndTime ");
                dynamicParams.Add("EndTime", requset.EndTime);
            }

            sql.AppendFormat(@"SELECT count(1)
FROM `eagles`.`tb_test_paper`  where 1=1  {0} ;
 ", parameter);
            totalCount = dbManager.Excuted(sql.ToString(), dynamicParams);

            sql.Clear();

            sql.AppendFormat(@"SELECT `tb_test_paper`.`OrgId`,
    `tb_test_paper`.`BranchId`,
    `tb_test_paper`.`TestId`,
    `tb_test_paper`.`TestName`,
    `tb_test_paper`.`HasReward`,
    `tb_test_paper`.`QuestionSocre`,
    `tb_test_paper`.`PassScore`,
    `tb_test_paper`.`HasLimitedTime`,
    `tb_test_paper`.`LimitedTime`,
    `tb_test_paper`.`HtmlDescription`,
    `tb_test_paper`.`OperId`,
    `tb_test_paper`.`CreateTime`,
    `tb_test_paper`.`EditTime`,
    `tb_test_paper`.`TestType`,
    `tb_test_paper`.`PassAwardScore`,
    `tb_test_paper`.`Status`
FROM `eagles`.`tb_test_paper`  where 1=1  {0} order by CreateTime desc limit  (@pageNum-1)*@pageSize ,@pageNum;
 ", parameter);

            return dbManager.Query<TB_TEST_PAPER>(sql.ToString(), dynamicParams);
        }



        public List<TB_QUESTION> GetSubjectListByQuestionId(List<int> questionId)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@"SELECT `tb_question`.`OrgId`,
    `tb_question`.`QuestionId`,
    `tb_question`.`Question`,
    `tb_question`.`Answer`,
    `tb_question`.`Multiple`,
    `tb_question`.`MultipleCount`
FROM `eagles`.`tb_question` where QuestionId  in @QuestionId;
 ");

            dynamicParams.Add("QuestionId", new {QuestionId = questionId.ToArray()});

            return dbManager.Query<TB_QUESTION>(sql.ToString(), dynamicParams);
        }

        public TB_TEST_PAPER GetExercisesDetail(GetExercisesDetailRequset requset)
        {

            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_test_paper`.`OrgId`,
    `tb_test_paper`.`BranchId`,
    `tb_test_paper`.`TestId`,
    `tb_test_paper`.`TestName`,
    `tb_test_paper`.`HasReward`,
    `tb_test_paper`.`QuestionSocre`,
    `tb_test_paper`.`PassScore`,
    `tb_test_paper`.`HasLimitedTime`,
    `tb_test_paper`.`LimitedTime`,
    `tb_test_paper`.`HtmlDescription`,
    `tb_test_paper`.`OperId`,
    `tb_test_paper`.`CreateTime`,
    `tb_test_paper`.`EditTime`,
    `tb_test_paper`.`Status`
FROM `eagles`.`tb_test_paper` where TestId=@TestId;
 ");
            dynamicParams.Add("TestId", new { TestId = requset.ExercisesId});

            return dbManager.Query<TB_TEST_PAPER>(sql.ToString(), dynamicParams).FirstOrDefault();
        }

        public int RemoveExercises(RemoveExercisesRequset requset)
        {
            return dbManager.Excuted(@"DELETE FROM `eagles`.`tb_test_paper`
WHERE TestId=@TestId;
", new {TestId = requset.ExercisesId});
        }

        public int EditExercises(TB_TEST_PAPER info)
        {
            return dbManager.Excuted(@"UPDATE `eagles`.`tb_test_paper`
            SET
                `OrgId` = @OrgId,
                `BranchId` = @BranchId,
                `TestName` = @TestName,
                `HasReward` = @HasReward,
                `QuestionSocre` = @QuestionSocre,
                `PassScore` = @PassScore,
                `HasLimitedTime` = @HasLimitedTime,
                `LimitedTime` = @LimitedTime,
                `HtmlDescription` = @HtmlDescription,
                `OperId` = @OperId,
                `CreateTime` = @CreateTime,
                `EditTime` = @EditTime,
                `Status` = @Status
                WHERE `TestId` = @TestId;
            ", info);
        }

        public int CreateExercises(TB_TEST_PAPER info)
        {
            return dbManager.Excuted(@"INSERT INTO `eagles`.`tb_test_paper`
(`OrgId`,
`BranchId`,
`TestId`,
`TestName`,
`HasReward`,
`QuestionSocre`,
`PassScore`,
`HasLimitedTime`,
`LimitedTime`,
`HtmlDescription`,
`OperId`,
`CreateTime`,
`EditTime`,
`Status`)
VALUES
(@OrgId,
@BranchId,
@TestId,
@TestName,
@HasReward,
@QuestionSocre,
@PassScore,
@HasLimitedTime,
@LimitedTime,
@HtmlDescription,
@OperId,
@CreateTime,
@EditTime,
@Status);

", info);
        }

        public int EditSubject(TB_QUESTION info)
        {

            return dbManager.Excuted(@" UPDATE `eagles`.`tb_question`
            SET
                `OrgId` = @OrgId,
                `Question` = @Question,
                `Answer` = @Answer,
                `Multiple` = @Multiple,
                `MultipleCount` = @MultipleCount
                WHERE `QuestionId` = @QuestionId;
", info);
          
        }

        public int CreateSubject(TB_QUESTION info)
        {
            return dbManager.Excuted(@"INSERT INTO `eagles`.`tb_question`
(`OrgId`,
`QuestionId`,
`Question`,
`Answer`,
`Multiple`,
`MultipleCount`)
VALUES
(@OrgId,
@QuestionId,
@Question,
@Answer,
@Multiple,
@MultipleCount);
", info);
        }

        public int RemoveSubject(RemoveSubjectRequset requset)
        {
            return dbManager.Excuted(@"DELETE FROM `eagles`.`tb_question` 
WHERE QuestionId=@QuestionId;", new { requset.QuestionId });
        }

        public TB_QUESTION GetSubjectDetail(GetSubjectDetailRequset requset)
        {

            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_question`.`OrgId`,
    `tb_question`.`QuestionId`,
    `tb_question`.`Question`,
    `tb_question`.`Answer`,
    `tb_question`.`Multiple`,
    `tb_question`.`MultipleCount`
FROM `eagles`.`tb_question`  where QuestionId=@QuestionId;
 ");
            dynamicParams.Add("QuestionId", new { requset.QuestionId });

            return dbManager.Query<TB_QUESTION>(sql.ToString(), dynamicParams).FirstOrDefault();
        }

        public List<TB_QUEST_ANWSER> GetOptionList(List<int> list)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_quest_anwser`.`OrgId`,
    `tb_quest_anwser`.`QuestionId`,
    `tb_quest_anwser`.`AnswerId`,
    `tb_quest_anwser`.`Answer`,
    `tb_quest_anwser`.`AnswerType`,
    `tb_quest_anwser`.`IsRight`,
    `tb_quest_anwser`.`ImageUrl`
FROM `eagles`.`tb_quest_anwser`  WHERE QuestionId in @QuestionId;
 ");
            dynamicParams.Add("QuestionId", new { QuestionId = list.ToArray() });

            return dbManager.Query<TB_QUEST_ANWSER>(sql.ToString(), dynamicParams);
        }

        public int RemoveOptionByQuestionId(int questionId)
        {
            return dbManager.Excuted(@" DELETE FROM `eagles`.`tb_quest_anwser`
WHERE QuestionId=@QuestionId;", new { QuestionId = questionId });
        }

        public int CreateOption(List<TB_QUEST_ANWSER> optionList)
        {
            return dbManager.Excuted(@"INSERT INTO `eagles`.`tb_quest_anwser`
(`OrgId`,
`QuestionId`,
`AnswerId`,
`Answer`,
`AnswerType`,
`IsRight`,
`ImageUrl`)
VALUES
(@OrgId,
@QuestionId,
@AnswerId,
@Answer,
@AnswerType,
@IsRight,
@ImageUrl);", optionList);

        }

        public int EditOption(List<TB_QUEST_ANWSER> optionList)
        {
            return dbManager.Excuted(@"UPDATE `eagles`.`tb_quest_anwser`
SET
`OrgId` = @OrgId,
`AnswerId` =@AnswerId,
`Answer` = @Answer,
`AnswerType` = @AnswerType,
`IsRight` = @IsRight,
`ImageUrl` =@ImageUrl
WHERE `AnswerId` = @AnswerId;", optionList);
        }

        public List<TB_TEST_QUESTION> GetTestQuestionRelationshipByTestId(int testId)
        {
            return dbManager.Query<TB_TEST_QUESTION>(@"SELECT `tb_test_question`.`OrgId`,
    `tb_test_question`.`TestId`,
    `tb_test_question`.`QuestionId`
FROM `eagles`.`tb_test_question` where TestId=@testId ", new { testId });
        }

        public int RemoveTestQuestionRelationshipByTestId(int testId)
        {
            return dbManager.Excuted(@" DELETE FROM `eagles`.`tb_test_question`
WHERE TestId=@TestId;", new { TestId = testId });
        }

        public int CreateTestQuestionRelationship(List<TB_TEST_QUESTION> list)
        {
            return dbManager.Excuted(@"  INSERT INTO `eagles`.`tb_test_question`
(`OrgId`,
`TestId`,
`QuestionId`)
VALUES
(@OrgId,
@TestId,
@QuestionId);", list);
        }

        public int RemoveTestQuestionRelationshipByQuestionId(int questionId)
        {
            return dbManager.Excuted(@" DELETE FROM `eagles`.`tb_test_question`
WHERE QuestionId=@QuestionId;", new { QuestionId = questionId });
        }

        public List<TB_QUESTION> GetRandomSubject(List<int> list, int count)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_question`.`OrgId`,
    `tb_question`.`QuestionId`,
    `tb_question`.`Question`,
    `tb_question`.`Answer`,
    `tb_question`.`Multiple`,
    `tb_question`.`MultipleCount`
FROM `eagles`.`tb_question`  where QuestionId not IN  @QuestionId   ORDER BY rand() LIMIT @count;
 ");
            dynamicParams.Add("QuestionId", new { QuestionId = list.ToArray() });
            dynamicParams.Add("count", count);

            return dbManager.Query<TB_QUESTION>(sql.ToString(), dynamicParams);
        }
    }
}
