using System.Xml.Serialization;
using Eagles.Base.Configuration;

namespace Eagles.Base.Config
{
    [XmlRoot("DataBaseConfig")]
    [XmlPath("/Configuration/DataBase.config")]
    public class DataBaseConfig
    {
        public string DataBaseConnectString { get; set; }

        /// <summary>
        /// token 有效时间
        /// </summary>
        public double EffectiveTime { get; set; }
        
    }
}
