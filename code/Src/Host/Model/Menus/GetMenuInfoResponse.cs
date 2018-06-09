using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Menus
{
    public class GetMenuInfoResponse:ResponseBase
    {
        public MenuInfo info { get; set; }
    }
}
