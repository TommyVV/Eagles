using System.Collections.Generic;
using Eagles.Application.Model.Publicity.Model;

namespace Eagles.Application.Model.Publicity.Response
{
    public class GetPublicActivityResponse
    {
        public List<PublicActivity> Activitys { get; set; }

        public int TotalCount { get; set; }
    }
}
