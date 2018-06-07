using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.SystemMessage
{
    public class GetSystemMessageInfoDetailsResponse:ResponseBase
    {
        public SystemMessageInfoDetails info { get; set; }
    }
}
