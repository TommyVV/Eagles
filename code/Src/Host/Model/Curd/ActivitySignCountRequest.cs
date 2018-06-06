using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    /// <summary>
    /// 活动报名人数查询
    /// </summary>
    class ActivitySignCountRequest : RequestBase
    {
        public string Token { get; set; }
        public string ActivityId { get; set; }
    }
}
