using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Audit.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAuditRequest : RequestBase
    {
        /// <summary>
        /// 审核名字
        /// </summary>
        public string AuditName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AuditStatus AuditStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OperationType OperationType { get; set; }
    }
}
