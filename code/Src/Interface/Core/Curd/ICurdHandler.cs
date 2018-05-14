using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Curd;
using Eagles.Base;
namespace Eagles.Interface.Core.Curd
{
    public interface ICurdHandler:IInterfaceBase
    {
        GetInfosRespone GetInfos();

        AfterInfoResponse AfterInfo(AfterInfoRequest info);

        GetInfoResponse GetInfo(GetInfoRequest id);

        RemoveInfoResponse RemoveInfo(RemoveInfoRequest id);
    }
}
