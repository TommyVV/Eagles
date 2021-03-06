﻿using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Task.GetTaskStep
{
    /// <summary>
    /// 任务步骤查询
    /// </summary>
    public class GetTaskStepResponse 
    {
        /// <summary>
        /// 步骤列表
        /// </summary>
        public List<Step> StepList { get; set; }
    }
}