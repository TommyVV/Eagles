using System.Collections.Generic;

namespace Eagles.Application.Model.Test
{
    public class TestResponse: ResponseBase
    {
        public List<AreaInfo> AreaInfo { get; set; }

        public string TestNode { get; set; }
    }

    public class AreaInfo
    {
        /// <summary>
        /// area id
        /// </summary>
        public string AreaId { get; set; }


        /// <summary>
        /// area name 
        /// </summary>
        public string AreaName { get; set; }
    }
}
