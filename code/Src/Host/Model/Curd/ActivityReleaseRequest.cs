using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    class ActivityReleaseRequest : RequestBase
    {
        public string Token { get; set; }

        public string OrgId { get; set; }

        public string BranchId { get; set; }

        public string ActivityType { get; set; }

        public string ActivityName { get; set; }

        public string ActivityUser { get; set; }

        public string ActivityStartDate { get; set; }

        public string ActivityEndDate { get; set; }

        public string ActivityContent { get; set; }

        public string CanComment { get; set; }

        public string IsPublic { get; set; }

        public List<string> AttachList { get; set; }
    }
}
