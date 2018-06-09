﻿using System;

namespace Eagles.Application.Model.Curd.Activity.GetActivityDetail
{
    /// <summary>
    /// 活动详情查询
    /// </summary>
    public class GetActivityDetailRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public string ActivityId { get; set; }

        /// <summary>
        /// 密文UserId
        /// </summary>
        public string EncryptUserid { get; set; }
    }
}