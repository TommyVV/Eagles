using System;
using System.Linq;
using Eagles.Base;
using Eagles.Base.DesEncrypt;
using Eagles.Interface.Core.User;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.DataAccess.UserInfo;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.User.Login;
using Eagles.Application.Model.User.Register;
using Eagles.Application.Model.User.EditUser;
using Eagles.Application.Model.User.GetUserInfo;
using Eagles.Application.Model.User.GetUserRelationship;
using Eagles.Base.Configuration;
using Eagles.DomainService.Model.User;

namespace Eagles.DomainService.Core.User
{
    public class UserHandler : IUserHandler
    {
        private readonly IUtil util;
        private readonly IDesEncrypt desEncrypt;
        private readonly IUserInfoAccess userInfoAccess;
        private readonly IConfigurationManager Config;

        public UserHandler(IUserInfoAccess userInfoAccess, IUtil util, IDesEncrypt desEncrypt, IConfigurationManager config)
        {
            this.desEncrypt = desEncrypt;
            Config = config;
            this.userInfoAccess = userInfoAccess;
            this.util = util;
        }

        public EditUserResponse EditUser(EditUserRequest request)
        {
            var response = new EditUserResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.Code = "96";
                response.Message = "获取Token失败";
                return response;
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
                OriginAddress = reqUserInfo.CensusAddress,
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
            if (result > 0)
            {
                response.Code = "00";
                response.Message = "修改成功";
            }
            else
            {
                response.Code = "96";
                response.Message = "失败";
            }
            return response;
        }

        public GetUserInfoResponse GetUserInfo(GetUserInfoRequest request)
        {
            var response = new GetUserInfoResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.Code = "96";
                response.Message = "获取Token失败";
                return response;
            }
            if (request.AppId <= 0)
                throw new TransactionException("01", "AppId不允许为空");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
            var result = userInfoAccess.GetUserInfo(tokens.UserId);
            var userInfo = new UserInfo();
            userInfo.Name = result.Name;
            userInfo.Gender = result.Sex;
            userInfo.Birth = result.Birthday;
            userInfo.Telphone = result.Phone;
            userInfo.Address = result.Address;
            userInfo.Origin = result.Origin;
            userInfo.CensusAddress = result.OriginAddress;
            userInfo.Ethnic = result.Ethnic;
            userInfo.Branch = result.BranchId;
            userInfo.Department = result.Dept;
            userInfo.Education = result.Education;
            userInfo.School = result.School;
            userInfo.IdCard = result.IdNumber;
            userInfo.Employer = result.Company;
            userInfo.PrepPartyDate = result.PreMemberTime;
            userInfo.FormalPartyDat = result.MemberTime;
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
            var userId = request.UserId;
            var pwd = request.UserPwd;
            var result = userInfoAccess.GetLogin(request.UserId);
            if (result != null)
            {
                var password1 = desEncrypt.Encrypt(result.Password);
                var password2 = desEncrypt.Encrypt(pwd);

                if (!password1.Equals(password2))
                {
                    throw new TransactionException("96", "账户密码错误");
                }

                //登录新增Token
                var userToken = new TbUserToken()
                {
                    UserId = userId,
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
                    throw new TransactionException("96", "登陆失败");
                }

                //返回前端加密userId
                response.EncryptUserid = desEncrypt.Encrypt(userId.ToString());
            }
            else
            {
                throw new TransactionException("96", "查无此账号");
            }

            return response;
        }

        public RegisterResponse Register(RegisterRequest request)
        {
            var response = new RegisterResponse();
            var userInfo = new TbUserInfo();
            var phone = request.Phone;
            var code = request.SmsCode;
            var result = userInfoAccess.CreateUser(userInfo);
            return response;
        }

        public GetUserRelationshipResponse GetUserRelationship(GetUserRelationshipRequest request)
        {
            var response = new GetUserRelationshipResponse();
            var userId = Convert.ToInt32(desEncrypt.Decrypt(request.UserId));
            var result = userInfoAccess.GetRelationship(userId);
            response.UserRelationshipList = result?.Select(x =>
            new UserRelationship
            {
                UserId = x.UserId,
                SubUserId = x.SubUserId
            }).ToList();
            return null;
        }
    }
}