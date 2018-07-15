using Eagles.Base;
using Eagles.Application.Model.Export;

namespace Eagles.Interface.Core.ExportFile
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExportHandler : IInterfaceBase
    {
        ExportResponse ExportFile(ExportRequest request);
    }
}