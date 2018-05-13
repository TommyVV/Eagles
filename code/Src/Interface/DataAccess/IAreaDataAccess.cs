using System.Collections.Generic;
using Eagles.Base;
using Ealges.Infrastructure.DataBaseModel.Model;

namespace Eagles.Interface.Infrastructure.DataAccess
{
    public interface IAreaDataAccess: IInterfaceBase
    {
        List<Area> GetAreas(string id);


    }
}
