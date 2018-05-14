using Eagles.Application.Model.PartyMission;
using Eagles.Base.Configuration;
using Eagles.Interface.Core.PartyMission;
using Eagles.Interface.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Eagles.DomainService.Core.PartyMission
{
    public class PartyMissionHandler : IPartyMissionHandler
    {
        private readonly IPartyMissionAccess pmData;

        private readonly IConfigurationManager configurationManager;

        public PartyMissionHandler(IPartyMissionAccess pmData, IConfigurationManager configurationManager)
        {
            this.pmData = pmData;
            this.configurationManager = configurationManager;
        }

        public PartyMissionResponse GetPartyMissionById(PartyMissionRequest request)
        {
            var respone = new PartyMissionResponse() { ErrorCode = "00", IsSuccess = "0", Message = "成功" };
            var info = pmData.GetPmInfo(request.PmId);
            if (info == null)
            {
                respone.ErrorCode = "99";
                respone.Message = "失败";
                respone.IsSuccess = "1";
            }
            else
            {
                respone.PmInfo = new PartyMissionInfo()
                {
                    PmId = info.PmId,
                    PmName = info.PmName
                };
            }

            return respone;
        }
    }

    [XmlRoot("Configuration")]
    [XmlPath("/Configuration/Test.config")]
    public class TestConfig
    {
        public string TestNode { get; set; }
    }
}
