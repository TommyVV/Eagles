using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Audit.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class EditAuditInfoRequset:RequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        public int AuditId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public AuditStatus AuditStatus { get; set; }

        /// <summary>
        /// 内容 json格式 图片 文字
        /// </summary>
        public string Content { get; set; }
    }
}
