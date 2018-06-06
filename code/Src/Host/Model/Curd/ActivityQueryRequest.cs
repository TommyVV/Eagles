using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    /// <summary>
    /// 活动查询
    /// </summary>
    class ActivityQueryRequest : RequestBase
    {
        public string Token { get; set; }

        public string UserId { get; set; }
    }
}
