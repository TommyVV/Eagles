using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using Eagles.Application.Model.Curd;
using Eagles.Base.Configuration;
using Eagles.Interface.Core.Curd;
using Eagles.Interface.Infrastructure.DataAccess;
using DBModel = Ealges.Infrastructure.DataBaseModel.Model;
namespace Eagles.DomainService.Core.testCurd
{

    public class CurdHandler: ICurdHandler
    {
        private readonly ICurdDataAccess curdData;

        private readonly IConfigurationManager configurationManager;
        public CurdHandler(ICurdDataAccess curdData, IConfigurationManager configurationManager)
        {
            this.curdData = curdData;
            this.configurationManager = configurationManager;
        }

        public GetInfosRespone GetInfos()
        {
            var respone = new GetInfosRespone() {ErrorCode = "00", IsSuccess = "0", Message = "成功",List=new List<UserInfo>()};
            var info = curdData.GetInfos();
            if (info.Count > 0)
            {
                info.ForEach(x => respone.List.Add(ConvertUserInfo(x)));
                respone.TotalCount = info.First().TotalCount;
                respone.IsSuccess = "1";
            }
            else
            {
                respone.ErrorCode = "失败";
            }
            return respone;
        }

        public AfterInfoResponse AfterInfo(AfterInfoRequest info)
        {
            var respone = new AfterInfoResponse() {ErrorCode = "00", IsSuccess = "0", Message = "成功"};
            var result = curdData.AfterInfo(new DBModel.UserInfo()
            {
                Age = info.UserInfo.Age,
                Gender = info.UserInfo.Gender,
                Id = info.UserInfo.Id,
                Name = info.UserInfo.Name
            });
            if (result > 0)
            {
                respone.IsSuccess = "1";
            }
            else
            {
                respone.ErrorCode = "失败";
            }

            return respone;
        }


        public GetInfoResponse GetInfo(GetInfoRequest request)
        {
            var respone = new GetInfoResponse() {ErrorCode = "00", IsSuccess = "0", Message = "成功"};
            var info = curdData.GetInfo(request.Id);
            if (info == null)
            {
                respone.ErrorCode = "失败";
            }
            else
            {
                respone.UserInfo = new UserInfo()
                {
                    Name = info.Name,
                    Id = info.Id,
                    Gender = info.Gender,
                    Age = info.Age
                };
                respone.IsSuccess = "1";
            }

            return respone;
        }

        public RemoveInfoResponse RemoveInfo(RemoveInfoRequest request)
        {
            var respone = new RemoveInfoResponse() {ErrorCode = "00", IsSuccess = "0", Message = "成功"};
            var result = curdData.RemoveInfo(request.Id);
            if (result > 0)
            {
                respone.IsSuccess = "1";
            }
            else
            {
                respone.ErrorCode = "失败";
            }
            return respone;
        }

        private UserInfo ConvertUserInfo(DBModel.UserInfo info)
        {
            return new UserInfo
            {
                Age = info.Age,
                Gender = info.Gender,
                Id = info.Id,
                Name = info.Name,
            };
        }

      
    }
}
