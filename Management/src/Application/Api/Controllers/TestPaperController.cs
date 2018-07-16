using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.Exercises.Requset;
using Eagles.Application.Model.Exercises.Response;
using Eagles.Interface.Core;

using Eagles.Application.Host.Common;
namespace Eagles.Application.Host.Controllers
{
    public class TestPaperController : ApiController
    {
        private ITestPaperHandler _testpaperHandler;

        public TestPaperController(ITestPaperHandler testpaperHandler)
        {
            _testpaperHandler = testpaperHandler;
        }

        #region 习题

        /// <summary>
        /// 习题详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetSubjectDetailResponse> GetSubjectDetail(GetSubjectDetailRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _testpaperHandler.GetSubjectDetail(requset));
        }

        /// <summary>
        /// 编辑习题
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<int> EditSubject(EditSubjectRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _testpaperHandler.EditSubject(requset));
        }

        /// <summary>
        /// 生产随机习题
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetRandomSubjectResponse> GetRandomSubject(GetRandomSubjectRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _testpaperHandler.GetRandomSubject(requset));
        }

        /// <summary>
        ///  习题角度 删除习题
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> RemoveSubjectInfo(RemoveSubjectInfoRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _testpaperHandler.RemoveSubjectInfo(requset));
        }


        /// <summary>
        /// 获取习题列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetSubjectListResponse> GetSubjectList(GetSubjectListRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _testpaperHandler.GetSubjectList(requset));
        }



        #endregion

        #region 试卷

        /// <summary>
        /// 编辑试卷
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<ResponseBase> EditExercises(EditExercisesRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _testpaperHandler.EditExercises(requset));
        }

        /// <summary>
        /// 删除试卷
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> RemoveExercises(RemoveExercisesRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _testpaperHandler.RemoveExercises(requset));
        }

        /// <summary>
        /// 试卷详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetExercisesDetailResponse> GetExercisesDetail(GetExercisesDetailRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _testpaperHandler.GetExercisesDetail(requset));
        }

        /// <summary>
        /// 试卷列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetExercisesResponse> GetExercises(GetExercisesRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _testpaperHandler.GetExercises(requset));
        }


        /// <summary>
        /// 删除习题
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> RemoveSubject(RemoveSubjectRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _testpaperHandler.RemoveSubject(requset));
        }


        #endregion

        #region 选项


        ///// <summary>
        ///// 编辑选项
        ///// </summary>
        ///// <param name="requset"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ResponseFormat<int> EditOption(EditOptionRequset requset)
        //{
        //    return ApiActuator.Runing(requset, (requset1) => _testpaperHandler.EditOption(requset));
        //}

        ///// <summary>
        ///// 删除选项
        ///// </summary>
        ///// <param name="requset"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ResponseFormat<bool> RemoveOption(RemoveOptionRequset requset)
        //{
        //    return ApiActuator.Runing(requset, (requset1) => _testpaperHandler.RemoveOption(requset));
        //}





        #endregion



    }
}