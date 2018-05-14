using Eagles.Application.Model.PartyMission;
using Eagles.Base;

namespace Eagles.Interface.Core.PartyMission
{
    public interface IPartyMissionHandler : IInterfaceBase
    {
        PartyMissionResponse GetPartyMissionById(PartyMissionRequest id);
    }
}
