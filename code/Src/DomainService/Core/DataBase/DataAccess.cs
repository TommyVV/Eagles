using System.Data;
using Eagles.Base.CustomAttr;
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
            var t=dbManager.Query<Test>("select * from tb_oper where oper_id=@id", new {id = new int[1] {10000000}});
        }
    }

    
    public class Test
    {
        [Column(Name = "oper_id")]
        public string OperId { get; set; }
    }
}
