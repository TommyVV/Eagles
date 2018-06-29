using System.Web.Http;
using Eagles.Interface.Core.Study;
using Eagles.Application.Model.Study.EditStudyTime;
using Eagles.Application.Model.Study.GetStudyTime;

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
        public EditStudyTimeResponse EditStudyTime(EditStudyTimeRequest request)
        {
            return iStudyHandler.EditStudyTime(request);
        }

        /// <summary>
        /// 学习时间查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public GetStudyTimeResponse GetStudyTime(GetStudyTimeRequest request)
        {
            return iStudyHandler.GetStudyTime(request);
        }
    }
}