using System.Xml.Serialization;
using Eagles.Base.Configuration;

namespace Eagles.Base.Config
{
    [XmlRoot("DataBaseConfig")]
    [XmlPath("/Configuration/DataBase.config")]
    public class DataBaseConfig
    {
        public string DataBaseConnectString { get; set; }
    }
}
