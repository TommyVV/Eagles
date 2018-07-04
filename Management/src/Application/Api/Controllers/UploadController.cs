using System.Web;
using System.Web.Http;
using Eagles.Application.Model.Upload;
using Eagles.Base;
using Eagles.DomainService.Core.FileUpload;
using Eagles.Interface.Core.FileUpload;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 上传文件
    /// </summary>
    public class UploadController : ApiController
    {
        private readonly IFileUploadHandler fileUpload;

        public UploadController(IFileUploadHandler fileUpload)
        {
            this.fileUpload = fileUpload;
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        [HttpPost,HttpOptions]
        public ResponseFormat<UploadResponse> UploadFile()
        {
            //set allow post 
            HttpContext.Current.Response.Headers.Add("Allow", "POST");
            return ApiActuator.Runing(() => fileUpload.Process());
        }
    }
}