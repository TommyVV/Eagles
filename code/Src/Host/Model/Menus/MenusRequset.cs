using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Menus
{
    public class MenusRequset:RequestBase
    {
        /// <summary>
        /// 机构号
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 菜单级别 
        /// </summary>
        public MenuLevel MenuLevel { get; set; }
    }
}
