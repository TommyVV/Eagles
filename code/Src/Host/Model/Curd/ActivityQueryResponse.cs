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
    class ActivityQueryResponse : ResponseBase
    {
        public List<Activity> ActivityList { get; }
    }

    class Activity
    {
        string ActivityId { get; set; }

        string ActivityTitle { get; set; }

        string ActivityType { get; set; }

        string ActivityContent { get; set; }

        string ActivityDate { get; set; }

        string ActivityImg { get; set; }
    }

}
