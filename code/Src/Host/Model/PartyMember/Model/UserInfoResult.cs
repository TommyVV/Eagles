using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.PartyMember.Model;

namespace Eagles.Application.Model.PartyMember
{
    public class UserInfoResult:Member
    {
        /// <summary>
        /// 检查结果
        /// </summary>
        public string Result { get; set; }

        public bool IsSystemUser { get; set; }
    }
}
