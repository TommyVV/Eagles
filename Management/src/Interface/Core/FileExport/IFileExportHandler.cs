using Eagles.Base;
using Eagles.Application.Model.Export;

namespace Eagles.Interface.Core.FileExport
{
    public interface IFileExportHandler : IInterfaceBase
    {
        ExportResponse ExportFile(ExportRequest request);
    }
}