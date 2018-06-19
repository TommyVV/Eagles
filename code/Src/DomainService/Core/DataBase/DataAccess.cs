using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.Base.DataBase.Modle;
using Eagles.Interface.Core.DataBase;

namespace Eagles.DomainService.Core.DataBase
{
    public class DataAccess : IDataAccess
    {
        private readonly IDbManager dbManager;

        public DataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public void GetAreas(string id)
        {
            var t=dbManager.ExecuteScalar<int>(@"INSERT INTO `eagles`.`tb_test`
                (
                `Name`)
            VALUES
            (
                '1'); select LAST_INSERT_ID(); ",null);
        }
    }
}