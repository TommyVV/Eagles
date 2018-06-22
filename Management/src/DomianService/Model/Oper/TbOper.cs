using System;

namespace Eagles.DomainService.Model.Oper
{
    /// <summary>
    /// TB_OPER
    /// </summary>
    public class TbOper
    {
        public DateTime CreateTime { get; set; }
        public int GroupId { get; set; }
        public int OperId { get; set; }
        public string OperName { get; set; }
        public int OrgId { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
    }
}