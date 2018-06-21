using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Eagles.Base.Configuration.Implement
{
    public class ConfigurationManager : IConfigurationManager
    {
        public T GetConfiguration<T>()
        {
            var path=GetConfigPath<T>();
            var realpath = AppDomain.CurrentDomain.BaseDirectory+path;
            return ReadXml<T>(realpath);
        }

        private string GetConfigPath<T>()
        {
            var s = typeof(T).CustomAttributes;
            foreach (var customAttributeData in s)
            {
                if (customAttributeData.AttributeType == typeof(XmlPath))
                {
                    return customAttributeData.ConstructorArguments?[0].Value as string;

                }
            }

            return null;
        }

        private T ReadXml<T>(string path)
        {
            try
            {
                var x = new XmlDocument();
                x.Load(path);
                object xml;
                using (var sr = new StringReader(x.OuterXml))
                {
                    var xmlSerializer = new XmlSerializer(typeof(T));
                    xml = xmlSerializer.Deserialize(sr);
                    sr.Close();
                }
                return (T)xml;
            }
            catch (Exception e)
            {
                return default(T);
            }

        }
    }
}
