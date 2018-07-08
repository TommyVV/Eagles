using System;
using Eagles.Application.Model.Enums;

namespace Eagles.DomainService.Model.App
{
    /// <summary>
    /// TB_APP_MENU
    /// </summary>
    public class TbAppMenu
    {
        public String Level { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int OrgId { get; set; }
        public int ParentMenuId { get; set; }
        public string TargetUrl { get; set; }
    }
}