using System.Xml.Serialization;
using Eagles.Base.Configuration;

namespace Eagles.DomainService.Model.Config
{
    [XmlRoot("Configuration")]
    [XmlPath("/Configuration/DataBase.config")]
    public class DataBaseConfig
    {
        public string SqlConnectString { get; set; }
    }
}
