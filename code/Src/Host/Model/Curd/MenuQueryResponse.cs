using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    class MenuQueryResponse : ResponseBase
    {
        public string Token { get; set; }
        public string OrgId { get; set; }
        public string OrgName { get; set; }
        public string MenuId { get; set; }

        /// <summary>
        /// 菜单类型 1-一级菜单 2-二级菜单
        /// </summary>
        public string MenuType { get; set; }
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }

    }
}
