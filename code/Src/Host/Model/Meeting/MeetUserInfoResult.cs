﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Meeting
{
    public class MeetUserInfoResult: MeetUserInfoDetails
    {
        /// <summary>
        /// 检查结果
        /// </summary>
        public string Result { get; set; }
    }
}