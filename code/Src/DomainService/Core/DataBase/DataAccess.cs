using System.Data;
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

        private IDbCommand command;

        public void GetAreas(string id)
        {
            var t=dbManager.Query<Test>("select * from tb_oper where oper_id=@id", new {id = new[] {10000000}});
        }
    }

    public class Test
    {
        public string Oper_Id { get; set; }
    }
}
