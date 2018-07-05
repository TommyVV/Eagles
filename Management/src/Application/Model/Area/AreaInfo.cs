using System.Collections.Generic;

namespace Eagles.Application.Model.Area
{
    public class AreaInfo
    {
        public string value { get; set; }

        public string label { get; set; }

        public List<City> children { get; set; }
    }

    public class City
    {
        public string value { get; set; }

        public string label { get; set; }

        public List<District> children { get; set; }

    }

    public class District
    {
        public string value { get; set; }

        public string label { get; set; }
    }
}
