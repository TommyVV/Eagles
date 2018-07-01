using System.Xml.Serialization;
using Eagles.Base.Configuration;

namespace Eagles.DomainService.Model.Config
{
    [XmlRoot("Eagles")]
    [XmlPath("/Configuration/Eagles.config")]
    public class EaglesConfiguration
    {
        public string FilePath { get; set; }

        public string ImageBaseUrl { get; set; }
    }
}
