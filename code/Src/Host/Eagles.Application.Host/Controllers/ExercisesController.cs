using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.Exercises.Requset;
using Eagles.Application.Model.Exercises.Response;
using Eagles.Interface.Core;

namespace Eagles.Host.Controllers
{
    public class ExercisesController : ApiController
    {
        private IExercisesHandler ExercisesHandler;

        #region

        /// <summary>
        /// 试卷详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/GetSubjectDetail")]
        [HttpGet]
        public GetSubjectDetailResponse GetSubjectDetail(GetSubjectDetailRequset requset)
        {
            return ExercisesHandler.GetSubjectDetail(requset);
        }

        /// <summary>
        /// 删除试卷
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/RemoveSubject")]
        [HttpGet]
        ResponseBase RemoveSubject(RemoveSubjectRequset requset)
        {
            return ExercisesHandler.RemoveSubject(requset);
        }

        /// <summary>
        /// 编辑试卷
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/EditSubject")]
        [HttpGet]
        public ResponseBase EditSubject(EditSubjectRequset requset)
        {
            return ExercisesHandler.EditSubject(requset);
        }
        #endregion

        #region 习题

        /// <summary>
        /// 编辑习题
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/EditExercises")]
        [HttpGet]
        ResponseBase EditExercises(EditExercisesRequset requset)
        {
            return ExercisesHandler.EditExercises(requset);
        }

        /// <summary>
        /// 删除习题
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/RemoveExercises")]
        [HttpGet]
        public ResponseBase RemoveExercises(RemoveExercisesRequset requset)
        {
            return ExercisesHandler.RemoveExercises(requset);
        }

        /// <summary>
        /// 习题详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/GetExercisesDetail")]
        [HttpGet]
        public GetExercisesDetailResponse GetExercisesDetail(GetExercisesDetailRequset requset)
        {
            return ExercisesHandler.GetExercisesDetail(requset);
        }

        /// <summary>
        /// 习题列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/GetExercises")]
        [HttpGet]
        public GetExercisesResponse GetExercises(GetExercisesRequset requset)
        {
            return ExercisesHandler.GetExercises(requset);
        }

        #endregion

        /// <summary>
        /// 生产随机习题
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/GetRandomSubject")]
        [HttpGet]
        public GetRandomSubjectResponse GetRandomSubject(GetRandomSubjectRequset requset)
        {
            return ExercisesHandler.GetRandomSubject(requset);
        }

    }
}