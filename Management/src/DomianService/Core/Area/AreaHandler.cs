using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Eagles.Application.Model.Area;
using Eagles.Base.Json;
using Eagles.DomainService.Model.Area;
using Eagles.Interface.Core.Area;

namespace Eagles.DomainService.Core.Area
{
    public class AreaHandler: IAreaHandler
    {
        private readonly IJsonSerialize jsonSerialize;

        public AreaHandler(IJsonSerialize jsonSerialize)
        {
            this.jsonSerialize = jsonSerialize;
        }

        public AreaResponse Process()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory+ "Configuration/Data/city.min.js";
            var file = File.ReadAllText(path);
            var areaData=jsonSerialize.Deserialize<AreaData>(file);
            if (areaData == null)
            {
                return null;
            }

            var response = new AreaResponse()
            {
                AreaInfos = new List<AreaInfo>()
            };
            areaData.citylist.ForEach(x =>
            {
                response.AreaInfos.Add(new AreaInfo()
                {
                    label = x.p,
                    value = x.p,
                    children =x.c?.Select(y=>new Application.Model.Area.City
                    {
                        label = y.n,
                        value = y.n,
                        children = y.a?.Select(z =>new Application.Model.Area.District
                        {
                            label = z.s,
                            value = z.s
                        }).ToList()
                    }).ToList()
                });
            });
            return response;
        }
    }
}
