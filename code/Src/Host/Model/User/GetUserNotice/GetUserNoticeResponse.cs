using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.User.GetUserNotice
{
    /// <summary>
    /// 用户通知接口查询
    /// </summary>
    public class GetUserNoticeResponse
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        public List<UserNotice> NoticeList { get; set; }
    }
}