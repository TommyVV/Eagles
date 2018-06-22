using System;

namespace Eagles.Application.Model.SystemMessage.Model
{
    public class SystemMessageInfo
    {
        public int SystemMessageId { get; set; }

        /// <summary>
        /// 系统通知标题
        /// </summary>
        public int SystemMessageName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 内容 json格式 图片 文字
        /// </summary>
        public string Content { get; set; }
    }
}
