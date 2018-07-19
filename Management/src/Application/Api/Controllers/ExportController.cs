using System.Web.Http;
using Eagles.Application.Host.Common;
using Eagles.Application.Model.Export;
using Eagles.Interface.Core.FileExport;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 导出控制器
    /// </summary>
    public class ExportController : ApiController
    {
        private readonly IFileExportHandler exportHandler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exportHandler"></param>
        public ExportController(IFileExportHandler exportHandler)
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