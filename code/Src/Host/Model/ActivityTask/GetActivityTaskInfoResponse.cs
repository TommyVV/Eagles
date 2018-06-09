using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.ActivityTask
{
    public class GetActivityTaskInfoResponse:ResponseBase
    {
        public ActivityTaskInfoDetails info { get; set; }
    }
}
