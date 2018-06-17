using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Exercises.Model;
using Eagles.Application.Model.Exercises.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.Exercises;
using Eagles.DomainService.Model.TB_TEST;

namespace Eagles.Interface.Core.DataBase
{
    public interface IExercisesDataAccess : IInterfaceBase
    {
        List<TB_TEST_PAPER> GetExercisesList(GetExercisesRequset requset,out int totalCount);
        List<TB_QUESTION> GetSubjectListByQuestionId(List<int> questionId);
        TB_TEST_PAPER GetExercisesDetail(GetExercisesDetailRequset requset);
        int RemoveExercises(RemoveExercisesRequset requset);
        int EditExercises(TB_TEST_PAPER info);
        int CreateExercises(TB_TEST_PAPER info);
        int EditSubject(TB_QUESTION info);
        int CreateSubject(TB_QUESTION info);
        int RemoveSubject(RemoveSubjectRequset requset);
        TB_QUESTION GetSubjectDetail(GetSubjectDetailRequset requset);
        List<TB_QUEST_ANWSER> GetOptionList(List<int> ints);
        int RemoveOptionByQuestionId(int questionId);
        int CreateOption(List<TB_QUEST_ANWSER> infoOptionList);
        int EditOption(List<TB_QUEST_ANWSER> infoOptionList);
        List<TB_TEST_QUESTION> GetTestQuestionRelationshipByTestId(int testId);
        int RemoveTestQuestionRelationshipByTestId(int testId);
        int CreateTestQuestionRelationship(List<TB_TEST_QUESTION> list);
        int RemoveTestQuestionRelationshipByQuestionId(int questionId);
        List<TB_QUESTION> GetRandomSubject(List<int> list,int count);
    }
}
