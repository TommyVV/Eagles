using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Meeting
{
    public class MeetUserInfoDetails : MeetUserInfo
    {
        /// <summary>
        /// 是否为系统人员
        /// </summary>
        public bool IsSystemUser { get; set; }
    }
}
