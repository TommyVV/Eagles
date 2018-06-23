using Eagles.Base;

namespace Eagles.Interface.DataAccess
{
    public interface IDataAccess:IInterfaceBase
    {
        void GetAreas(string id);
    }
}
