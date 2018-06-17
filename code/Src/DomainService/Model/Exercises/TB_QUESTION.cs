﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.DomainService.Model.Exercises
{
    public class TB_QUESTION
    {
        /// <summary>
        /// 答案类型;0:默认:1:自定义
        /// </summary>
        public int Answer { get; set; }

        /// <summary>
        /// 是否允许多选;0:否;1:是
        /// </summary>
        public int Multiple { get; set; }
        /// <summary>
        /// 多选数量
        /// </summary>
        public int MultipleCount { get; set; }
        /// <summary>
        /// 机构ID
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 问题
        /// </summary>
        public string Question { get; set; }
        /// <summary>
        /// 问题id
        /// </summary>
        public int QuestionId { get; set; }
    }
}
