using System;

namespace Eagles.Application.Model.Curd.GetSocreRank
{
    /// <summary>
    /// 积分排行查询
    /// </summary>
    public class GetSocreRankRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        
    }
}