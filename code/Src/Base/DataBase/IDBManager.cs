using System.Collections.Generic;
using Eagles.Base.DataBase.Modle;

namespace Eagles.Base.DataBase
{
    public interface IDbManager : IInterfaceBase
    {
        List<T> Query<T>(string command, object parameter=null);

        T QuerySingle<T>(string command, object parameter = null);

        int Excuted(string command, object parameter=null);

        T ExecuteScalar<T>(string command, object parameter);

        T ExecuteScalar<T>(string command, object parameter);

        bool ExcutedByTransaction(List<TransactionCommand> command);
    }
}