using Eagles.Interface.Infrastructure.DataAccess;
using Ealges.Infrastructure.DataBaseModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Infrastructure.DataAccess
{
    class PartyMissionDapper : IPartyMissionAccess
    {
        private List<PartyMission> _list = new List<PartyMission>
        {
            new PartyMission()
            {
                PmId = 1,
                PmName = "第一次全国人民代表大会"
            },
            new PartyMission()
            {
                PmId = 2,
                PmName = "第二次全国人民代表大会"
            }
        };

        public PartyMission GetPmInfo(int id)
        {
            return _list.Find(x => x.PmId == id);
        }
    }
}