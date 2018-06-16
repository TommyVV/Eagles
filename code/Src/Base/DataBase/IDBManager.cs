using System.Collections.Generic;

namespace Eagles.Base.DataBase
{
    public interface IDbManager:IInterfaceBase
    {
        List<T> Query<T>(string command, object parameter=null);

        int Excuted(string command, object parameter=null);

        bool ExcutedByTransaction(Dictionary<string, object> command);
    }
}
