using System.Collections.Generic;
using System.Xml.Serialization;
using Eagles.Base.Configuration;

namespace Eagles.DomainService.Model.Config
{
    [XmlRoot("Menus")]
    [XmlPath("/Configuration/FunctionMenu.xml")]
    public class FunctionMenu
    {
        [XmlElement]
        public List<Node> MenuNodes { get; set; }
    }
   
    public class Node
    {
        [XmlAttribute]
        public string Priority { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement]
        public List<Menu> Menu { get; set; }
    }

    public class Menu
    {
        [XmlAttribute]
        public string Priority { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string FunCode { get; set; }
    }

}
