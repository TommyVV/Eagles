using System;
using System.Data;
using Eagles.Application.Model.Export;
using Eagles.Interface.Core.FileExport;

namespace Eagles.DomainService.Core.FileExport
{
    public class FileExportHandler : IFileExportHandler
    {
        public ExportResponse ExportFile(ExportRequest request)
        {

            DataTable dataTable = new DataTable();



            return new ExportResponse() { ExportFilePath = "" };
        }
    }
}