using Eagles.Base;
using Ealges.Infrastructure.DataBaseModel.Model;

namespace Eagles.Interface.Infrastructure.DataAccess
{
    public interface IPartyMissionAccess: IInterfaceBase
    {
        PartyMission GetPmInfo(int id);
    }
}