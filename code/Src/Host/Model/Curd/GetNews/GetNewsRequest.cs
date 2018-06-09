using System;
using Eagles.Application.Model.Curd.Enum;

namespace Eagles.Application.Model.Curd.GetNews
{
    /// <summary>
    /// 文章列表查询
    /// </summary>
    class GetNewsRequest : RequestBase
    {
        public string Token { get; set; }

        /// <summary>
        /// 文章类型 00:文章 01:心得体会 02:会议
        /// </summary>
        public NewsEnum NewsType { get; set; }
    }
}