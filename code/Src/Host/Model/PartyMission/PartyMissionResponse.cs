using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.PartyMission
{
    public class PartyMissionResponse:ResponseBase
    {
        public PartyMissionInfo PmInfo { get; set; }
    }

    public class PartyMissionInfo
    {
        public int PmId { get; set; }
        
        public string PmName { get; set; }
    }
}
