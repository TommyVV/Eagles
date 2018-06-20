﻿
namespace Eagles.Application.Model.AppModel.News
{
    /// <summary>
    /// 新闻详情查询
    /// </summary>
    public class GetNewsDetailRequest : RequestBase
    {
        /// <summary>
        /// 新闻编号
        /// </summary>
      public int NewsId { get; set; }
    }
}