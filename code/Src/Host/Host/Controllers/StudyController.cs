using System.Web.Http;
using Eagles.Interface.Core.Study;
using Eagles.Application.Model.Study.EditStudyTime;
using Eagles.Application.Model.Study.GetStudyTime;
using Eagles.Base;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 学习控制器
    /// </summary>
    [ValidServiceToken]
    public class StudyController : ApiController
    {
        private IStudyHandler iStudyHandler;

        public StudyController(IStudyHandler iStudyHandler)
        {
            this.iStudyHandler = iStudyHandler;
        }

        /// <summary>
        /// 学习时间记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<EditStudyTimeResponse> EditStudyTime(EditStudyTimeRequest request)
        {
            return ApiActuator.Runing(() => iStudyHandler.EditStudyTime(request));
           // return iStudyHandler.EditStudyTime(request);
        }

        /// <summary>
        /// 学习时间查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetStudyTimeResponse> GetStudyTime(GetStudyTimeRequest request)
        {
            return ApiActuator.Runing(() => iStudyHandler.GetStudyTime(request));
        }
    }
}