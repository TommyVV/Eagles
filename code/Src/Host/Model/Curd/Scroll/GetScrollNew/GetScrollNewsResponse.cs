﻿using System;

namespace Eagles.Application.Model.Curd.Scroll.GetScrollNew
{
    /// <summary>
    /// 滚动消息查询
    /// </summary>
    public class GetScrollNewsResponse : ResponseBase
    {
        /// <summary>
        /// 消息Id
        /// </summary>
        public int NewsId { get; set; }

        /// <summary>
        /// 消息名称
        /// </summary>
        public string NewsName { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string NewsContent { get; set; }
    }
}