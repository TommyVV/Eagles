using System.Collections.Generic;
using Eagles.Application.Model.News;

namespace Eagles.Application.Model.Curd.News
{
    public class GetModuleNewsResponse:ResponseBase
    {
        public List<NewsInfo> NewsInfos { get; set; }
    }
}
