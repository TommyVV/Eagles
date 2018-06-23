using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.Exercises.Requset;
using Eagles.Application.Model.Exercises.Response;
using Eagles.Interface.Core;

namespace Eagles.Application.Host.Controllers
{
    public class TestPaperController : ApiController
    {
        private ITestPaperHandler _testpaperHandler;

        public TestPaperController(ITestPaperHandler testpaperHandler)
        {
            _testpaperHandler = testpaperHandler;
        }

        #region

        /// <summary>
        /// 试卷详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public GetSubjectDetailResponse GetSubjectDetail(GetSubjectDetailRequset requset)
        {
            return _testpaperHandler.GetSubjectDetail(requset);
        }

        /// <summary>
        /// 删除试卷
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        ResponseBase RemoveSubject(RemoveSubjectRequset requset)
        {
            return _testpaperHandler.RemoveSubject(requset);
        }

        /// <summary>
        /// 编辑试卷
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseBase EditSubject(EditSubjectRequset requset)
        {
            return _testpaperHandler.EditSubject(requset);
        }
        #endregion

        #region 习题

        /// <summary>
        /// 编辑习题
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        ResponseBase EditExercises(EditExercisesRequset requset)
        {
            return _testpaperHandler.EditExercises(requset);
        }

        /// <summary>
        /// 删除习题
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseBase RemoveExercises(RemoveExercisesRequset requset)
        {
            return _testpaperHandler.RemoveExercises(requset);
        }

        /// <summary>
        /// 习题详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public GetExercisesDetailResponse GetExercisesDetail(GetExercisesDetailRequset requset)
        {
            return _testpaperHandler.GetExercisesDetail(requset);
        }

        /// <summary>
        /// 习题列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public GetExercisesResponse GetExercises(GetExercisesRequset requset)
        {
            return _testpaperHandler.GetExercises(requset);
        }

        #endregion

        /// <summary>
        /// 生产随机习题
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public GetRandomSubjectResponse GetRandomSubject(GetRandomSubjectRequset requset)
        {
            return _testpaperHandler.GetRandomSubject(requset);
        }

    }
}