﻿using Eagles.Application.Model.ActivityTask.Model;

namespace Eagles.Application.Model.ActivityTask.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetActivityTaskDetailResponse : ResponseBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ActivityTaskDetailModel Info { get; set; }
    }
}
