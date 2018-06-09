using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Audit
{
    public class AlterAuditInfoRequset
    {
        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }

        public int AuditId { get; set; }


        public AuditStatus AuditStatus { get; set; }

        /// <summary>
        /// 内容 json格式 图片 文字
        /// </summary>
        public string Content { get; set; }
    }
}
