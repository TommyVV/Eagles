using System;

namespace Eagles.Application.Model.SystemMessage.Model
{
    public class SystemNews
    {
        /// <summary>
        /// 消息id（新增时无需传入）
        /// </summary>
        public int NewsId { get; set; }

        /// <summary>
        /// 消息名称
        /// </summary>
        public string NewsName { get; set; }
        
        /// <summary>
        /// 提示日期
        /// </summary>
        public string NoticeTime { get; set; }
        /// <summary>
        /// 状态 0：正常 1：禁用
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 重复类型;
        ///  0:每年;
        ///  1:仅1次
        /// </summary>
        public int RepeatTime { get; set; }

        /// <summary>
        /// HTML描述（富文本）
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
