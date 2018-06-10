using System;
using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Curd.GetNews
{
    /// <summary>
    /// 文章列表查询
    /// </summary>
    class GetNewsResponse :ResponseBase
    {
        public List<Common.News> NewsList { get; }
    }
}