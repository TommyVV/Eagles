using System.Collections.Generic;

namespace Eagles.Base.DataBase
{
    public interface IDbManager:IInterfaceBase
    {
        List<T> Query<T>(string command, object parameter);

        int Excuted(string command, object paramster);
    }
}
