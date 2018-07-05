using System.Collections.Generic;

namespace Eagles.DomainService.Model.Area
{
    public class AreaData
    {
        public List<Province> citylist { get; set; }
    }

    public class Province
    {
        public string p { get; set; }

        public List<City> c { get; set; }
    }

    public class City
    {
        public string n { get; set; }

        public List<District> a { get; set; }
    }

    public class District
    {
        public string s { get; set; }
    }
}
