using System.Collections.Generic;
using Eagles.Application.Model.PartyMember.Model;

namespace Eagles.Application.Model.PartyMember.Requset
{
    public class ImportUserRequest:RequestBase
    {
        /// <summary>
        /// 导入支部id
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// 导入用户列表
        /// </summary>
        public List<ImportUser> UserList { get; set; }
    }
}
