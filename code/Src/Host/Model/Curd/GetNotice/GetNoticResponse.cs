using System;

namespace Eagles.Application.Model.Curd.GetNotice
{
    /// <summary>
    /// 通知公告查询
    /// </summary>
    public class GetNoticResponse :ResponseBase
    {
        /// <summary>
        /// 通知Id
        /// </summary>
        public string NewsId { get; set; }

        /// <summary>
        /// 通知类型
        /// </summary>
        public string NewsType { get; set; }

        /// <summary>
        /// 通知标题
        /// </summary>
        public string NewsTitle { get; set; }

        /// <summary>
        /// 通知接收人
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 通知内容
        /// </summary>
        public string NewsContent { get; set; }

        /// <summary>
        /// 通知发起人
        /// </summary>
        public string FromUser { get; set; }

        /// <summary>
        /// 是否阅读
        /// </summary>
        public string IsRead { get; set; }
    }
}