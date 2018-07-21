using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.DomainService.Model.Activity
{
    public class TbTask
    {
        public string Attach1 { get; set; }
        public string Attach2 { get; set; }
        public string Attach3 { get; set; }
        public string Attach4 { get; set; }
        public string AttachName1 { get; set; }
        public string AttachName2 { get; set; }
        public string AttachName3 { get; set; }
        public string AttachName4 { get; set; }
        public DateTime BeginTime { get; set; }
        public int BranchId { get; set; }
        public string BranchReview { get; set; }
        public int CanComment { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreateType { get; set; }
        public DateTime EndTime { get; set; }
        public int FromUser { get; set; }
        public string FromUserName { get; set; }
        public int IsPublic { get; set; }
        public int OrgId { get; set; }
        public string OrgReview { get; set; }
        public int Status { get; set; }
        public string TaskContent { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
    }
}
