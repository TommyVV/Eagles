using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.Exercises;
using Eagles.Application.Model.Exercises.Requset;

namespace Eagles.Interface.Core.DataBase
{
    public interface IExercisesDataAccess : IInterfaceBase
    {
        List<TbTestPaper> GetExercisesList(GetExercisesRequset requset,out int totalCount);
        List<TbQuestion> GetSubjectListByQuestionId(List<int> questionId);
        TbTestPaper GetExercisesDetail(GetExercisesDetailRequset requset);
        int RemoveExercises(RemoveExercisesRequset requset);
        int EditExercises(TbTestPaper info);
        int CreateExercises(TbTestPaper info);
        int EditSubject(TbQuestion info);
        int CreateSubject(TbQuestion info);
        int RemoveSubject(RemoveSubjectRequset requset);
        TbQuestion GetSubjectDetail(GetSubjectDetailRequset requset);
        List<TbQuestAnswer> GetOptionList(List<int> ints);
        int RemoveOptionByQuestionId(int questionId);
        int CreateOption(List<TbQuestAnswer> infoOptionList);
        int EditOption(List<TbQuestAnswer> infoOptionList);
        List<TbTestQuestion> GetTestQuestionRelationshipByTestId(int testId);
        int RemoveTestQuestionRelationshipByTestId(int testId);
        int CreateTestQuestionRelationship(List<TbTestQuestion> list);
        int RemoveTestQuestionRelationshipByQuestionId(int questionId);
        List<TbQuestion> GetRandomSubject(List<int> list,int count);
    }
}
