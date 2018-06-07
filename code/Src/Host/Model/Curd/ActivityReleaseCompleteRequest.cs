using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    class ActivityReleaseCompleteRequest : RequestBase
    {
        public string Token { get; set; }

        public string OrgId { get; set; }

        public string BranchId { get; set; }

        public string ActivityId { get; set; }
    }
}
