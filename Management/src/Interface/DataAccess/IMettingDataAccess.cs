using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Meeting.Model;
using Eagles.Application.Model.Meeting.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.User;

namespace Eagles.Interface.DataAccess
{
    public interface IMettingDataAccess: IInterfaceBase
    {
        List<TbUserInfo> GetUserInfoByPhone(List<string> list);
        int CreateUserInfo(List<TbMeetingUser> userinfo);
        List<TbMeetingUser> GetMettingUsers(GetMeetingRequest requset);
    }
}
