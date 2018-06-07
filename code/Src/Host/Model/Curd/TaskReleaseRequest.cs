using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    class TaskReleaseRequest : RequestBase
    {
        public string Token { get; set; }

        public string OrgId { get; set; }

        public string BranchId { get; set; }

        public string TaskName { get; set; }

        public string TaskUser { get; set; }

        public string TaskStartDate { get; set; }

        public string TaskEndDate { get; set; }

        public string TaskContent { get; set; }

        public string CanComment { get; set; }

        public string IsPublic { get; set; }

        public List<string> AttachList { get; set; }
    }
}
