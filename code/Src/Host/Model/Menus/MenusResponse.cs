using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Menus
{
    public class MenusResponse:ResponseBase
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<MenuInfo> List { get; set; }
    }
}
