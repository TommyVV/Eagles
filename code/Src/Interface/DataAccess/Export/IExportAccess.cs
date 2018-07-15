using System.Collections.Generic;
using Eagles.Base;

namespace Eagles.Interface.DataAccess.Export
{
    public interface IExportAccess : IInterfaceBase
    {
        List<T> ExportInfo<T>(string sql);
    }
}