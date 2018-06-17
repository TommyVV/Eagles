using System;

namespace Eagles.Application.Model.Curd.Scroll.GetScrollNew
{
    /// <summary>
    /// 滚动消息查询
    /// </summary>
    public class GetScrollNewsRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }
}