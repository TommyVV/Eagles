﻿using System;

namespace Eagles.Application.Model.Operator.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Operator
    {
        /// <summary>
        /// 操作员编号
        /// </summary>
        public int OperId { get; set; }

        /// <summary>
        /// 操作员名称
        /// </summary>
        public string OperName { get; set; }

        ///// <summary>
        ///// 权限组名称
        ///// </summary>
        //public string AuthorityGroupName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

    }
}
