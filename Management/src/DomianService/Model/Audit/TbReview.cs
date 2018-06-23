using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.DomainService.Model.Audit
{
    public class TbReview
    {
        /// <summary>
        /// 支部id
        /// </summary>
        public int BranchId { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 新闻id
        /// </summary>
        public int NewsId { get; set; }
        /// <summary>
        /// 审核类型
        ///00:文章
        ///10:任务
        ///20:活动
        /// </summary>
        public string NewsType { get; set; }
        /// <summary>
        /// 审核用户
        /// </summary>
        public int OperId { get; set; }
        /// <summary>
        /// 组织id
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 审核结果
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 审核流水id
        /// </summary>
        public int ReviewId { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public AuditStatus ReviewStatus { get; set; }
    }
}
