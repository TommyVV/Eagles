using System;
using System.Collections.Generic;
using Eagles.Application.Model.Test;
using Eagles.Interface.Core.Test;

namespace Eagles.DomainService.Core.Test
{
    public class TestHandler: ITestHandler
    {
        public TestResponse Porcess(TestRequest request)
        {
            var result = new TestResponse()
            {
                AreaInfo = new List<AreaInfo>()
                {

                },
                DateTime = DateTime.Now,
            };
            return result;
        }
    }

  
}
