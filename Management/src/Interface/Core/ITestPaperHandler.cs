using Eagles.Application.Model;
using Eagles.Application.Model.Exercises.Requset;
using Eagles.Application.Model.Exercises.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface ITestPaperHandler : IInterfaceBase
    {

        #region 试卷

        ///// <summary>
        ///// 试卷列表
        ///// </summary>
        ///// <param name="requset"></param>
        ///// <returns></returns>
        //GetSubjectResponse GetSubjectList(GetSubjectRequset requset);

        /// <summary>
        /// 试卷详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetSubjectDetailResponse GetSubjectDetail(GetSubjectDetailRequset requset);

        /// <summary>
        /// 删除试卷
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        bool RemoveSubject(RemoveSubjectRequset requset);

        /// <summary>
        /// 编辑试卷
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        int EditSubject(EditSubjectRequset requset);
        #endregion

        #region 试卷

        /// <summary>
        /// 编辑试卷
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        ResponseBase EditExercises(EditExercisesRequset requset);

        /// <summary>
        /// 删除试卷
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        bool RemoveExercises(RemoveExercisesRequset requset);

        /// <summary>
        /// 试卷详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetExercisesDetailResponse GetExercisesDetail(GetExercisesDetailRequset requset);

        /// <summary>
        /// 试卷列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetExercisesResponse GetExercises(GetExercisesRequset requset);
        #endregion

        /// <summary>
        /// 生产随机习题
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetRandomSubjectResponse GetRandomSubject(GetRandomSubjectRequset requset);
    }
}
