using System;
using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Curd.GetPartyInfo
{
    /// <summary>
    /// 党建学习查询
    /// </summary>
    public class GetPartyInfoResponse : ResponseBase
    {
        /// <summary>
        /// 党建学习列表
        /// </summary>
        public List<PartyInfo> PartyInfoList { get; }
    }
}