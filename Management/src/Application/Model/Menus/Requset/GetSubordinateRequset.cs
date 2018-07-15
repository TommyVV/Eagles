using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Menus.Requset
{

    /// <summary>
    /// 
    /// </summary>
    public class GetSubordinateRequset : RequestBase
    {
        /// <summary>
        /// 级菜单名称
        /// </summary>
        public int MenuId { get; set; }
    }
}
