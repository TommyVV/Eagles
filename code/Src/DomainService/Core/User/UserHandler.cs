using System;
using System.Linq;
using System.Collections.Generic;
using Eagles.Application.Model;
using Eagles.Base;
using Eagles.Base.Md5Helper;
using Eagles.Base.DesEncrypt;
using Eagles.Base.Configuration;
using Eagles.Interface.Core.User;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.DataAccess.UserInfo;
using Eagles.DomainService.Model.Sms;
using Eagles.DomainService.Model.User;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.User.Login;
using Eagles.Application.Model.User.Register;
using Eagles.Application.Model.User.EditUser;
using Eagles.Application.Model.User.GetUserInfo;
using Eagles.Application.Model.User.GetUserRelationship;

namespace Eagles.DomainService.Core.User
{
    public class UserHandler : IUserHandler
    {
        private readonly IUserInfoAccess userInfoAccess;
        private readonly IUtil util;
        private readonly IDesEncrypt desEncrypt;
        private readonly IMd5Helper md5Helper;
        private readonly IConfigurationManager config;

        public UserHandler(IUserInfoAccess userInfoAccess, IUtil util, IDesEncrypt desEncrypt, IMd5Helper md5Helper, IConfigurationManager config)
        {
            this.desEncrypt = desEncrypt;
            this.md5Helper = md5Helper;
            this.config = config;
            this.userInfoAccess = userInfoAccess;
            this.util = util;
        }

        public EditUserResponse EditUser(EditUserRequest request)
        {
            var response = new EditUserResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            }
            var reqUserInfo = request.RequestUserInfo;
            var userInfo = new TbUserInfo()
            {
                UserId = reqUserInfo.UserId,
                Name = reqUserInfo.Name,
                Sex = reqUserInfo.Gender,
                Birthday = reqUserInfo.Birth,
                Phone = reqUserInfo.Telphone,
                Address = reqUserInfo.Address,
                OriginAddress = reqUserInfo.OriginAddress,
                Ethnic = reqUserInfo.Ethnic,
                BranchId = reqUserInfo.Branch,
                Dept = reqUserInfo.Department,
                Education = reqUserInfo.Education,
                School = reqUserInfo.School,
                IdNumber = reqUserInfo.IdCard,
                Company = reqUserInfo.Employer,
                Origin = reqUserInfo.Origin,
                PreMemberTime = reqUserInfo.PrepPartyDate,
                MemberTime = reqUserInfo.FormalPartyDat,
                MemberType = reqUserInfo.PartyType,
                Provice = reqUserInfo.Provice,
                City = reqUserInfo.City,
                District = reqUserInfo.District,
                PhotoUrl = reqUserInfo.PhotoUrl
            };
            var result = userInfoAccess.EditUser(userInfo);
            if (result <= 0)
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            return response;
        }

        public GetUserInfoResponse GetUserInfo(GetUserInfoRequest request)
        {
            var response = new GetUserInfoResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidParameter);
            }
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var result = userInfoAccess.GetUserInfo(tokens.UserId);
            if (result == null)
                throw new TransactionException("01", "用户信息不存在");
            var userInfo = new UserInfo();
            userInfo.Name = result.Name;
            userInfo.Gender = result.Sex;
            userInfo.Birth = result.Birthday.ToLocalTime();
            userInfo.Telphone = result.Phone;
            userInfo.Address = result.Address;
            userInfo.Origin = result.Origin;
            userInfo.OriginAddress = result.OriginAddress;
            userInfo.Ethnic = result.Ethnic;
            userInfo.Branch = result.BranchId;
            userInfo.Department = result.Dept;
            userInfo.Education = result.Education;
            userInfo.School = result.School;
            userInfo.IdCard = result.IdNumber;
            userInfo.Employer = result.Company;
            userInfo.PrepPartyDate = result.PreMemberTime;
            userInfo.FormalPartyDat = result.MemberTime.ToLocalTime();
            userInfo.PartyType = result.MemberType;
            userInfo.Provice = result.Provice;
            userInfo.City = result.City;
            userInfo.District = result.District;
            userInfo.PhotoUrl = result.PhotoUrl;
            response.ResultUserInfo = userInfo;
            return response;
        }

        public LoginResponse Login(LoginRequest request)
        {
            var response = new LoginResponse();
            var guit = Guid.NewGuid().ToString();
            var result = userInfoAccess.GetLogin(request.Phone);
            if (result != null)
            {
                var password = md5Helper.Md5Encypt(request.UserPwd);
                if (!result.Password.Equals(password))
                    throw new TransactionException(MessageCode.UserNameOrPasswordError, MessageKey.UserNameOrPasswordError);
                //登录新增Token
                var userToken = new TbUserToken()
                {
                    UserId = result.UserId,
                    Token = guit,
                    CreateTime = DateTime.Now,
                    ExpireTime = DateTime.Now.AddMinutes(30),
                    TokenType = 0
                };
                var tokenInfo = userInfoAccess.InsertToken(userToken);
                if (tokenInfo > 0)
                {
                    response.Token = guit;
                }
                else
                {
                    throw new TransactionException(MessageCode.LoginFail, MessageKey.LoginFail);
                }
                //返回前端加密userId
                response.EncryptUserid = desEncrypt.Encrypt(result.UserId.ToString());
            }
            else
            {
                throw new TransactionException(MessageCode.LoginFail, MessageKey.LoginFail);
            }
            return response;
        }

        public RegisterResponse Register(RegisterRequest request)
        {
            var response = new RegisterResponse();
            var login = userInfoAccess.GetLogin(request.Phone);
            if (login != null)
                throw new TransactionException(MessageCode.ExistsPhone,MessageKey.ExistsPhone);
            var userInfo = new TbUserInfo()
            {
                Phone = request.Phone,
                Password = md5Helper.Md5Encypt(request.Pwd),
                CreateTime = DateTime.Now
            };
            var codeInfo = new TbValidCode() { Phone = request.Phone, Code = request.ValidCode, Seq = request.Seq };
            var resultCode = userInfoAccess.GetValidCode(codeInfo);
            if (resultCode == null)
                throw new TransactionException(MessageCode.InvalidCode, MessageKey.InvalidCode);
            var result = userInfoAccess.CreateUser(userInfo);
            return response;
        }

        public GetUserRelationshipResponse GetUserRelationship(GetUserRelationshipRequest request)
        {
            var response = new GetUserRelationshipResponse();
            var userId = Convert.ToInt32(desEncrypt.Decrypt(request.UserId));
            var superiorUserList = userInfoAccess.GetRelationship(userId, true);
            var lowerUserList = userInfoAccess.GetRelationship(userId, false);
            var slist = superiorUserList.Select(x => x.UserId).ToList();
            var llist = lowerUserList.Select(x => x.SubUserId).ToList();
            List<int> userIdList = slist.Union(llist).ToList();
            var userInfo = userInfoAccess.GetUserInfo(userIdList);
            response.SuperiorUserList = superiorUserList?.Select(x =>
                new UserRelationship
                {
                    UserId = x.UserId,
                    Name = userInfo.First(u => (u.UserId == x.UserId)).Name
                }).ToList();
            response.LowerUserList = lowerUserList?.Select(x =>
                new UserRelationship
                {
                    UserId = x.UserId,
                    Name = userInfo.First(u => (u.UserId == x.SubUserId)).Name
                }).ToList();
            return response;
        }
    }
}