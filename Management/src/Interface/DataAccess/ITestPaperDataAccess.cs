using System.Collections.Generic;
using Eagles.Application.Model.Exercises.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.Exercises;

namespace Eagles.Interface.DataAccess
{
    public interface ITestPaperDataAccess : IInterfaceBase
    {
        List<TbTestPaper> GetExercisesList(GetExercisesRequset requset,out int totalCount);
        List<TbQuestion> GetSubjectListByQuestionId(List<int> questionId);
        TbTestPaper GetExercisesDetail(GetExercisesDetailRequset requset);
        bool RemoveExercisesRelationship(RemoveExercisesRequset requset);
        bool RemoveExercisesSubjectRelationship(RemoveSubjectRequset requset);
        int EditExercises(TbTestPaper info);
        int CreateExercises(TbTestPaper info);
        int EditSubject(TbQuestion info);
        int CreateSubject(TbQuestion info);
        int RemoveSubject(RemoveSubjectRequset requset);
        TbQuestion GetSubjectDetail(GetSubjectDetailRequset requset);
        List<TbQuestAnswer> GetOptionList(List<int> ints);
        int RemoveOptionByQuestionId(int questionId);
        int CreateOption(TbQuestAnswer infoOptionList);
        int EditOption(TbQuestAnswer infoOptionList);
        List<TbTestQuestion> GetTestQuestionRelationshipByTestId(int testId);
        int RemoveTestQuestionRelationshipByTestId(int testId);
        int CreateTestQuestionRelationship(List<TbTestQuestion> list);
        int RemoveTestQuestionRelationshipByQuestionId(int questionId);
        List<TbQuestion> GetRandomSubject(List<int> list,int count);
        int RemoveOption(RemoveOptionRequset requset);
        void UpdataOption(int infoQuestionId, List<int> requsetOptionId);
        bool RemoveExercises(RemoveExercisesRequset requset);
    }
}
