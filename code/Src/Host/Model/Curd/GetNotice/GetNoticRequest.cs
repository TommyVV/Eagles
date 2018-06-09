using System;

namespace Eagles.Application.Model.Curd.GetNotice
{
    /// <summary>
    /// 通知公告查询
    /// </summary>
    public class GetNoticRequest :RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        
    }
}