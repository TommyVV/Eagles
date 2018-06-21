using System;
using Eagles.Application.Model.AppModel.Enum;

namespace Eagles.Application.Model.AppModel.News.GetNews
{
    /// <summary>
    /// 文章列表查询
    /// </summary>
    public class GetNewsRequest : RequestBase
    {
        /// <summary>
        /// 文章类型 00:文章 01:心得体会 02:会议
        /// </summary>
        public NewsEnum NewsType { get; set; }
    }
}