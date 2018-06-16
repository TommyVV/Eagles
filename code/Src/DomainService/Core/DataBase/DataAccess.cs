using Eagles.Base.DataBase;
using Eagles.Interface.Core.DataBase;

namespace Eagles.DomainService.Core.DataBase
{
    public class DataAccess:IDataAccess
    {
        private readonly IDbManager dbManager;

        public DataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public void GetAreas(string id)
        {
            var t = dbManager.Query<Model.Test>("select * from tb_oper where OperId=@id", new { id = new[] { 10000000 } });
            var operId = t[0].OperId;
        }
    }
}