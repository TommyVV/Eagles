using System;

namespace Eagles.Base.Configuration
{
    public class XmlPath:Attribute
    {
        public XmlPath(string path)
        {
            Path = path;
        }

        public string Path { get; set; }
    }
}
