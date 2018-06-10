using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Menus
{
    public class GetMenuInfoRequest
    {
        /// <summary>
        /// 级菜单名称
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }
    }
}
