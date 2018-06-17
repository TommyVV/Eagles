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
            var commands = new List<TransactionCommand>()
          {
              new TransactionCommand()
              {
                  CommandString = "Update tb_user_token set UserId=@userId where Token=@token",
                  Parameter =  new { userId = 20000000,token="abc001" }
              },
              new TransactionCommand()
              {
                  CommandString = "Update tb_oper set OperName=@operName where operId=@userId",
                  Parameter =  new { userId = 10000000,operName="tommy" }
              },
          };
            dbManager.ExcutedByTransaction(commands);
        }
    }
}