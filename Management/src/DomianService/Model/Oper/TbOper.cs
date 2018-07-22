using System;

namespace Eagles.DomainService.Model.Oper
{
    /// <summary>
    /// TB_OPER
    /// </summary>
    public class TbOper
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 群组id
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// 操作员编号
        /// </summary>
        public int OperId { get; set; }

        /// <summary>
        /// 操作员名称
        /// </summary>
        public string OperName { get; set; }
        /// <summary>
        /// 机构编号
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 状态;0:正常;1:禁用
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 失败次数
        /// </summary>
        public int LoginErrorCount { get; set; }

        /// <summary>
        /// 锁定时间
        /// </summary>
        public double LockingTime { get; set; }
    }
}