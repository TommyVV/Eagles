using Eagles.Base;

namespace Eagles.Interface.DataAccess.Test
{
    public interface IDataAccess:IInterfaceBase
    {
        void GetAreas(string id);
    }
}
