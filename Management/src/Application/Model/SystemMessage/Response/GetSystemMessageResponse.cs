using System.Collections.Generic;
using Eagles.Application.Model.SystemMessage.Model;

namespace Eagles.Application.Model.SystemMessage.Response
{
    public class GetSystemMessageResponse : ResponseBase
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<SystemMessageInfo> List { get; set; }
    }
}
