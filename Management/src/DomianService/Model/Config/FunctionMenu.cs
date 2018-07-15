using System.Collections.Generic;
using System.Xml.Serialization;
using Eagles.Base.Configuration;

namespace Eagles.DomainService.Model.Config
{
    [XmlRoot("Menus")]
    [XmlPath("/Configuration/FunctionMenu.xml")]
    public class FunctionMenu
    {
        public List<Node> Nodes { get; set; }
    }

   
    public class Node
    {
        //[XmlAttribute("Priority")]
        //public string Priority { get; set; }

        //[XmlAttribute("Name")]
        //public string Name { get; set; }

        [XmlElement("Menu")]
        public List<Menu> Menus { get; set; }
    }

    public class Menu
    {
        [XmlAttribute("Priority")]
        public string Priority { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("FunCode")]
        public string FunCode { get; set; }
    }

}
