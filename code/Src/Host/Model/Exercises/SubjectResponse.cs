﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Exercises
{
    public class SubjectResponse : RequestBase
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<Option> List { get; set; }
    }
}