﻿
namespace Eagles.Application.Model.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public enum TaskAcceptType
    {
        /// <summary>
        /// 上级审核任务
        /// </summary>
        Audit = 01,
        /// <summary>
        /// 下级接受任务
        /// </summary>
        Accept = 02,
        /// <summary>
        /// 下级申请完成
        /// </summary>
        Apply = 03
    }
}