using System;

namespace Eagles.Application.Model.Curd.GetScrollImg
{
    /// <summary>
    /// 滚动图片查询
    /// </summary>
    public class GetScrollImgRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }
}
