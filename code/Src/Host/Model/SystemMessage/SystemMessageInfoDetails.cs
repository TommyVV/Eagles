using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.enums;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.SystemMessage
{
    public class SystemMessageInfoDetails : SystemMessageInfo
    {
        public Status Status { get; set; }

        public MessageStatus MessageStatus { get; set; }
    }
}
