using System;
using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Curd.Activity.GetActivity
{
    /// <summary>
    /// 活动查询
    /// </summary>
    public class ActivityQueryResponse : ResponseBase
    {
        /// <summary>
        /// 活动列表
        /// </summary>
        public List<Common.Activity> ActivityList { get; }
    }
}