using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Base;
using Ealges.Infrastructure.DataBaseModel.Model;

namespace Eagles.Interface.Infrastructure.DataAccess
{
    public interface ICurdDataAccess : IInterfaceBase
    {
        List<UserInfo> GetInfos();

        int AfterInfo(UserInfo info);

        UserInfo GetInfo(string id);

        int RemoveInfo(string id);

    }
}
