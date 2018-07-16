using System.Web.Http;
using Eagles.Base;
using Eagles.Application.Model.Export;
using Eagles.Interface.Core.ExportFile;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 导出接口
    /// </summary>
    public class ExportController : ApiController
    {
        private readonly IExportHandler exportHandler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exportHandler"></param>
        public ExportController(IExportHandler exportHandler)
        {
            this.exportHandler = exportHandler;
        }

        /// <summary>
        /// 文件导出接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<ExportResponse> Export(ExportRequest request)
        {
            return ApiActuator.Runing(() => exportHandler.ExportFile(request));
        }
    }
}