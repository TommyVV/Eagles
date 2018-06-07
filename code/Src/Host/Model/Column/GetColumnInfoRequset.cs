using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Column
{
    class GetColumnInfoRequset
    {
        /// <summary>
        /// 栏目id
        /// </summary>
        public int ColumnId { get; set; }
        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }
    }
}
