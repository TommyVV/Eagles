using Eagles.Base.DataBase;
using Eagles.Interface.DataAccess.Test;

namespace Ealges.DomianService.DataAccess.DataBase
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
                @name); select LAST_INSERT_ID(); ",new{name="1"});
        }
    }
}