using System.Collections.Generic;
using Eagles.Application.Model.News.Model;

namespace Eagles.Application.Model.Curd.News
{
    /// <summary>
    /// 
    /// </summary>
    public class GetModuleNewsResponse:ResponseBase
    {
        public List<New> NewsInfos { get; set; }
    }
}
