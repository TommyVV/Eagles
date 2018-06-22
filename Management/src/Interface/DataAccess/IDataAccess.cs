using Eagles.Base;

namespace Eagles.Interface.Core.DataBase
{
    public interface IDataAccess:IInterfaceBase
    {
        void GetAreas(string id);
    }
}
