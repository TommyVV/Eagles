using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Operator
{
    public class OperatorInfo
    {
        /// <summary>
        /// 操作员编号
        /// </summary>
        public int OperId { get; set; }

        /// <summary>
        /// 操作员名称
        /// </summary>
        public int OperName { get; set; }

      

        /// <summary>
        /// 权限组名称
        /// </summary>
        public int AuthorityGroupName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }



    }
}
