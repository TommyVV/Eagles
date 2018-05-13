using System;
using System.Collections.Generic;
using System.Linq;
using Eagles.Interface.Infrastructure.DataAccess;
using Ealges.Infrastructure.DataBaseModel.Model;

namespace Eagles.Infrastructure.DataAccess
{
    public class CurdDapper : ICurdDataAccess
    {
        private  List<UserInfo> _list  = new List<UserInfo>
        {
            new UserInfo()
            {
                Age = 18,
                Gender = "男",
                Id = "1",
                Name = "小李"
            },
            new UserInfo()
            {
                Age = 23,
                Gender = "男",
                Id = "2",
                Name = "小明"
            },
            new UserInfo()
            {
                Age = 19,
                Gender = "男",
                Id = "3",
                Name = "小王"
            },
            new UserInfo()
            {
                Age = 19,
                Gender = "女",
                Id = "3",
                Name = "小嘟"
            }
        };

        public List<UserInfo> GetInfos()
        {
            
            return _list;
        }

        public int AfterInfo(UserInfo info)
        {
            var userInfo = _list.FirstOrDefault(x => x.Id == info.Id);
            _list.Remove(userInfo);
            _list.Add(info);
            return 1;
        }

        public UserInfo GetInfo(string id)
        {
            return _list.FirstOrDefault(x => x.Id == id);
        }

        public int RemoveInfo(string id)
        {
            var userInfo = _list.FirstOrDefault(x => x.Id == id);
            _list.Remove(userInfo);
            return 1;
        }
    }
}
