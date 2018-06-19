using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.PartyMember.Requset;
using Eagles.DomainService.Model.PartyMember;
using Eagles.Interface.Core.DataBase;

namespace Eagles.DomainService.Core.DataBase
{
    public class PartyMemberDataAccess:IPartyMemberDataAccess
    {
        public List<TB_USER_INFO> GetUserInfoList(GetPartyMemberRequest requset, out int totalCount)
        {
            throw new NotImplementedException();
        }

        public TB_USER_INFO GetUserInfoDetail(GetUserInfoDetailRequest requset)
        {
            throw new NotImplementedException();
        }

        public int RemoveUserInfo(RemoveUserInfoDetailsRequest requset)
        {
            throw new NotImplementedException();
        }

        public int EditUserInfo(TB_USER_INFO info)
        {
            throw new NotImplementedException();
        }

        public int CreateUserInfo(TB_USER_INFO info)
        {
            throw new NotImplementedException();
        }
    }
}
