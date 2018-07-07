using System;

namespace Eagles.Application.Model.SystemMessage.Model
{
    public class SystemNews
    {
        /// <summary>
        /// 消息id
        /// </summary>
        public int NewsId { get; set; }
        /// <summary>
        /// 消息名称
        /// </summary>
        public string NewsName { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string NewsContent { get; set; }
        /// <summary>
        /// 提示日期
        /// </summary>
        public DateTime NoticeTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public int OperId { get; set; }
        /// <summary>
        /// 重复类型;
        ///  0:每年;
        ///  1:仅1次
        /// </summary>
        public int RepeatTime { get; set; }
        /// <summary>
        /// HTML描述
        /// </summary>
        public string HtmlDesc { get; set; }
        /// <summary>
        /// 系统通知类型
        /// 00:领袖诞辰
        /// 10:系统通知
        /// </summary>
        public string NewsType { get; set; }
    }
}
