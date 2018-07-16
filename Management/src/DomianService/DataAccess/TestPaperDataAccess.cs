using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Eagles.Application.Model.Exercises.Requset;
using Eagles.Base.DataBase;
using Eagles.Base.DataBase.Modle;
using Eagles.DomainService.Model.Exercises;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{

    public class TestPaperDataAccess : ITestPaperDataAccess
    {
        private readonly IDbManager dbManager;

        public TestPaperDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<TbTestPaper> GetExercisesList(GetExercisesRequset requset, out int totalCount)
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

            if (!string.IsNullOrWhiteSpace(requset.ExercisesName))
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
                parameter.Append(" and CreateTime >= @StartTime ");
                dynamicParams.Add("StartTime", requset.StartTime);
            }

            if (requset.EndTime != null)
            {
                parameter.Append(" and CreateTime <= @EndTime ");
                dynamicParams.Add("EndTime", requset.EndTime);
            }

            sql.AppendFormat(@"SELECT count(*)
FROM `eagles`.`tb_test_paper`  where 1=1  {0} 
 ", parameter);
            totalCount = dbManager.ExecuteScalar<int>(sql.ToString(), dynamicParams);

            sql.Clear();

            dynamicParams.Add("pageStart", (requset.PageNumber - 1) * requset.PageSize);
            dynamicParams.Add("pageNum", requset.PageNumber);
            dynamicParams.Add("pageSize", requset.PageSize);


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
FROM `eagles`.`tb_test_paper`  where 1=1  {0} order by CreateTime desc limit  @pageStart ,@pageNum;
 ", parameter);

            return dbManager.Query<TbTestPaper>(sql.ToString(), dynamicParams);
        }

        public List<TbQuestion> GetSubjectListByQuestionId(List<int> questionId)
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

            dynamicParams.Add("QuestionId",  questionId.ToArray());

            return dbManager.Query<TbQuestion>(sql.ToString(), dynamicParams);
        }

        public TbTestPaper GetExercisesDetail(GetExercisesDetailRequset requset)
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
            dynamicParams.Add("TestId", new { TestId = requset.ExercisesId });

            return dbManager.Query<TbTestPaper>(sql.ToString(), dynamicParams).FirstOrDefault();
        }

        public bool RemoveExercisesRelationship(RemoveExercisesRequset requset)
        {
            return dbManager.ExcutedByTransaction(new List<TransactionCommand>()
            {               
                new TransactionCommand()
                {
                    CommandString = @"DELETE FROM `eagles`.`tb_test_question` WHERE TestId=@TestId;",
                    Parameter = new {TestId = requset.ExercisesId}
                },

            });
        }

        public bool RemoveExercises(RemoveExercisesRequset requset)
        {
            return dbManager.ExcutedByTransaction(new List<TransactionCommand>()
            {
                new TransactionCommand()
                {
                    CommandString = @"DELETE FROM `eagles`.`tb_test_question` WHERE TestId=@TestId;",
                    Parameter = new {TestId = requset.ExercisesId}
                },
                new TransactionCommand()
                {
                    CommandString = @"DELETE FROM `eagles`.`tb_test_paper` WHERE TestId=@TestId;",
                    Parameter = new {TestId = requset.ExercisesId}
                },

            });
        }

        public List<TbQuestion> GetSubjectList(GetSubjectListRequset requset, out int i)
        {
            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            if (requset.OrgId > 0)
            {
                parameter.Append(" and  OrgId = @OrgId ");
                dynamicParams.Add("OrgId", requset.OrgId);
            }



            if (!string.IsNullOrWhiteSpace(requset.QuestionId))
            {
                parameter.Append(" and TestName = @TestName ");
                dynamicParams.Add("TestName", requset.QuestionId);
            }

         
            if (requset.StartTime != null)
            {
                parameter.Append(" and CreateTime >= @StartTime ");
                dynamicParams.Add("StartTime", requset.StartTime);
            }

            if (requset.EndTime != null)
            {
                parameter.Append(" and CreateTime <= @EndTime ");
                dynamicParams.Add("EndTime", requset.EndTime);
            }

            sql.AppendFormat(@"SELECT count(*)
FROM `eagles`.`tb_question`  where 1=1  {0} 
 ", parameter);
            i = dbManager.ExecuteScalar<int>(sql.ToString(), dynamicParams);

            sql.Clear();

            dynamicParams.Add("pageStart", (requset.PageNumber - 1) * requset.PageSize);
            dynamicParams.Add("pageNum", requset.PageNumber);
            dynamicParams.Add("pageSize", requset.PageSize);


            sql.AppendFormat(@"SELECT `tb_question`.`OrgId`,
    `tb_question`.`QuestionId`,
    `tb_question`.`Question`,
    `tb_question`.`AnswerType`,
    `tb_question`.`Multiple`,
    `tb_question`.`MultipleCount`
FROM `eagles`.`tb_question`  where 1=1  {0} order by QuestionId desc limit  @pageStart ,@pageNum;
 ", parameter);

            return dbManager.Query<TbQuestion>(sql.ToString(), dynamicParams);
        }

        public bool RemoveExercisesSubjectRelationship(RemoveSubjectRequset requset)
        {
            return dbManager.ExcutedByTransaction(new List<TransactionCommand>()
            {
                new TransactionCommand()
                {
                    CommandString =
                        @"DELETE FROM `eagles`.`tb_test_question`  WHERE TestId=@TestId and QuestionId=@QuestionId",
                    Parameter = new {TestId = requset.ExercisesId, QuestionId = requset.QuestionId}
                },
                //new TransactionCommand()
                //{
                //    CommandString = @"DELETE FROM `eagles`.`tb_quest_anwser` WHERE QuestionId=@QuestionId;",
                //    Parameter =  new {requset.QuestionId}
                //},
            });
        }

        public int EditExercises(TbTestPaper info)
        {
            return dbManager.Excuted(@"UPDATE `eagles`.`tb_test_paper`
SET
`OrgId` = @OrgId,
`BranchId` = @BranchId,
`TestId` = @TestId,
`TestName` = @TestName,
`HasReward` = @HasReward,
`QuestionSocre` = @QuestionSocre,
`PassScore` = @PassScore,
`HasLimitedTime` = @HasLimitedTime,
`LimitedTime` = @LimitedTime,
`HtmlDescription` = @HtmlDescription,
`OperId` = @OperId,
`EditTime` = @EditTime,
`Status` = @Status,
`TestType` = @TestType,
`PassAwardScore` = @PassAwardScore,
`UserCount` = @UserCount
WHERE 
`TestId` = @TestId

            ", info);
        }

        public int CreateExercises(TbTestPaper info)
        {
            return dbManager.ExecuteScalar<int>(@"INSERT INTO `eagles`.`tb_test_paper`
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
`Status`,
`TestType`,
`PassAwardScore`,
`UserCount`)
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
now(),
now(),
@Status,
@TestType,
@PassAwardScore,
@UserCount);


select last_insert_id(); 
", info);
        }

        public int EditSubject(TbQuestion info)
        {

            return dbManager.Excuted(@" UPDATE `eagles`.`tb_question`
            SET
                `OrgId` = @OrgId,
                `Question` = @Question,
                `AnswerType` = @AnswerType,
                `Multiple` = @Multiple,
                `MultipleCount` = @MultipleCount
                WHERE `QuestionId` = @QuestionId;
", info);

        }

        public int CreateSubject(TbQuestion info)
        {
            return dbManager.ExecuteScalar<int>(@"INSERT INTO `eagles`.`tb_question`
(`OrgId`,
`QuestionId`,
`Question`,
`AnswerType`,
`Multiple`,
`MultipleCount`)
VALUES
(@OrgId,
@QuestionId,
@Question,
@AnswerType,
@Multiple,
@MultipleCount);

select last_insert_id(); 
", info);
        }

        public int RemoveSubject(RemoveSubjectRequset requset)
        {
            return dbManager.Excuted(@"DELETE FROM `eagles`.`tb_question` 
WHERE QuestionId=@QuestionId;", new { requset.QuestionId });
        }

        public TbQuestion GetSubjectDetail(GetSubjectDetailRequset requset)
        {

            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_question`.`OrgId`,
    `tb_question`.`QuestionId`,
    `tb_question`.`Question`,
    `tb_question`.`AnswerType`,
    `tb_question`.`Multiple`,
    `tb_question`.`MultipleCount`
FROM `eagles`.`tb_question`  where QuestionId=@QuestionId;
 ");
            dynamicParams.Add("QuestionId", requset.QuestionId);

            return dbManager.Query<TbQuestion>(sql.ToString(), dynamicParams).FirstOrDefault();
        }

        public List<TbQuestAnswer> GetOptionList(List<int> list)
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
            dynamicParams.Add("QuestionId", list.ToArray());

            return dbManager.Query<TbQuestAnswer>(sql.ToString(), dynamicParams);
        }

        public int RemoveOptionByQuestionId(int questionId)
        {
            return dbManager.Excuted(@" DELETE FROM `eagles`.`tb_quest_anwser`
WHERE QuestionId=@QuestionId;", new { QuestionId = questionId });
        }

        public int CreateOption(List<TbQuestAnswer> optionList)
        {
            return dbManager.ExecuteScalar<int>(@"INSERT INTO `eagles`.`tb_quest_anwser`
(`OrgId`,
`QuestionId`,
`AnswerId`,
`Answer`,
`AnswerType`,
`IsRight`,
`ImageUrl`,
`UserCount`)
VALUES
(@OrgId,
@QuestionId,
@AnswerId,
@Answer,
@AnswerType,
@IsRight,
@ImageUrl,
@UserCount);

select last_insert_id(); 
", optionList);

        }

        public int EditOption(TbQuestAnswer optionList)
        {
            return dbManager.Excuted(@"UPDATE `eagles`.`tb_quest_anwser`
SET
`OrgId` = @OrgId,
`QuestionId` = @QuestionId,
`Answer` = @Answer,
`AnswerType` = @AnswerType,
`IsRight` = @IsRight,
`ImageUrl` = @ImageUrl,
`UserCount` = @UserCount
WHERE `AnswerId` = @AnswerId", optionList);
        }

        public List<TbTestQuestion> GetTestQuestionRelationshipByTestId(int testId)
        {
            return dbManager.Query<TbTestQuestion>(@"SELECT `tb_test_question`.`OrgId`,
    `tb_test_question`.`TestId`,
    `tb_test_question`.`QuestionId`
FROM `eagles`.`tb_test_question` where TestId=@testId ", new { testId });
        }

        public int RemoveTestQuestionRelationshipByTestId(int testId)
        {
            return dbManager.Excuted(@" DELETE FROM `eagles`.`tb_test_question`
WHERE TestId=@TestId;", new { TestId = testId });
        }

        public int CreateTestQuestionRelationship(List<TbTestQuestion> list)
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

        public bool RemoveTestQuestionRelationshipByQuestionId(int questionId)
        {

            return dbManager.ExcutedByTransaction(new List<TransactionCommand>()
            {
                new TransactionCommand()
                {
                    CommandString =
                        @"DELETE FROM `eagles`.`tb_test_question`  WHERE QuestionId=@QuestionId",
                    Parameter = new {QuestionId = questionId}
                },
                new TransactionCommand()
                {
                    CommandString = @"DELETE FROM `eagles`.`tb_quest_anwser` WHERE QuestionId=@QuestionId;",
                    Parameter = new {QuestionId = questionId}
                },
                new TransactionCommand()
                {
                    CommandString = @"DELETE FROM `eagles`.`tb_question` WHERE QuestionId=@QuestionId;",
                    Parameter = new {QuestionId = questionId}
                },
            });

        }

        public List<TbQuestion> GetRandomSubject(List<int> list, int count)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_question`.`OrgId`,
    `tb_question`.`QuestionId`,
    `tb_question`.`Question`,
    `tb_question`.`AnswerType`,
    `tb_question`.`Multiple`,
    `tb_question`.`MultipleCount`
FROM `eagles`.`tb_question`  where QuestionId not IN  @QuestionId   ORDER BY rand() LIMIT @count;
 ");
            dynamicParams.Add("QuestionId", list.ToArray());
            dynamicParams.Add("count", count);

            return dbManager.Query<TbQuestion>(sql.ToString(), dynamicParams);
        }

        public int RemoveOption(List<int> requset)
        {
            return dbManager.Excuted(@"DELETE FROM `eagles`.`tb_quest_anwser`
WHERE AnswerId in @AnswerId;", new { AnswerId = requset.ToArray() });
        }

        public int RemoveOption(RemoveOptionRequset requset)
        {
            return dbManager.Excuted(@"DELETE FROM `eagles`.`tb_quest_anwser`
WHERE AnswerId = @AnswerId;", new { AnswerId = requset.OptionId });
        }

        public void UpdataOption(int infoQuestionId, List<int> requsetOptionId)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@"UPDATE `eagles`.`tb_quest_anwser`
SET
`QuestionId` = @QuestionId

WHERE `AnswerId` in @AnswerId;
 ");
            dynamicParams.Add("AnswerId", requsetOptionId.ToArray());
            dynamicParams.Add("QuestionId", infoQuestionId);

            dbManager.Excuted(sql.ToString(), dynamicParams);
        }
    }
}
