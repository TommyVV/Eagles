using System;

namespace Eagles.DomainService.Model.ImportLog
{
    /// <summary>
    /// TB_IMPORT_LOG
    /// </summary>
    public class ImportLog
    {
        public int BranchId { get; set; }
        public DateTime CreateTime { get; set; }
        public string Description { get; set; }
        public int OperId { get; set; }
        public string OperType { get; set; }
        public int OrgId { get; set; }
        public int Status { get; set; }
        public int TraceId { get; set; }
    }
}