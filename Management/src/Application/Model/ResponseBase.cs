﻿using System;

namespace Eagles.Application.Model
{
    /// <summary>
    /// 基本的返回
    /// </summary>
    public class ResponseBase
    {
        public ResponseBase()
        {
            IsSuccess = true;
        }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime DateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 返回码
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}
