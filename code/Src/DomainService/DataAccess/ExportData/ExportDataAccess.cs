using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.Interface.DataAccess.Export;

namespace Ealges.DomianService.DataAccess.ExportData
{
    public class ExportDataAccess : IExportAccess
    {
        private readonly IDbManager dbManager;

        public ExportDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<T> ExportInfo<T> (string sql)
        {
            return dbManager.Query<T>(sql, new { });
        }
    }
}