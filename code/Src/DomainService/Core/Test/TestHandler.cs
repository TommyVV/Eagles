using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Eagles.Application.Model.Test;
using Eagles.Base.Configuration;
using Eagles.Interface.Core.DataBase;
using Eagles.Interface.Core.Test;

namespace Eagles.DomainService.Core.Test
{
    public class TestHandler: ITestHandler
    {
        private readonly IDataAccess dataAccess;

        private readonly IConfigurationManager configurationManager;

        public TestHandler(IDataAccess dataAccess, IConfigurationManager configurationManager)
        {
            this.dataAccess = dataAccess;
            this.configurationManager = configurationManager;
        }

        public TestResponse Porcess(TestRequest request)
        {
            var s=configurationManager.GetConfiguration<TestConfig>();
            dataAccess.GetAreas("");
            var result = new TestResponse()
            {
                AreaInfo = new List<AreaInfo>()
                {

                },
                DateTime = DateTime.Now,
                TestNode = s.TestNode
            };
            return result;
        }
    }

    [XmlRoot("Configuration")]
    [XmlPath("/Configuration/Test.config")]
    public class TestConfig
    {
        public string TestNode { get; set; }
    }
}
