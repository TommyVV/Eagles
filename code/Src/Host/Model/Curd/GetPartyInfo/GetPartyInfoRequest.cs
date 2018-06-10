using System;

namespace Eagles.Application.Model.Curd.GetPartyInfo
{
    /// <summary>
    /// 党信息查询
    /// </summary>
    public class GetPartyInfoRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 模块类型 1-党建学习 2-党建门户 3-党务工作
        /// </summary>
        public string ModuleType { get; set; }
    }
}