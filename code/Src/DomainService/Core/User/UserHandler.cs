using System;
using System.Collections.Generic;
using Eagles.Base;
using Eagles.Base.Md5Helper;
using Eagles.Interface.Core.User;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.DataAccess.UserInfo;
using Eagles.DomainService.Model.Sms;
using Eagles.DomainService.Model.User;
using Eagles.Application.Model;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.User.Login;
using Eagles.Application.Model.User.Register;
using Eagles.Application.Model.User.EditUser;
using Eagles.Application.Model.User.GetUserInfo;
using Eagles.Application.Model.User.GetUserRelationship;
using Eagles.DomainService.Core.Utility;

namespace Eagles.DomainService.Core.User
{
    public class UserHandler : IUserHandler
    {
        private readonly IUserInfoAccess userInfoAccess;
        private readonly IUtil util;
        private readonly IMd5Helper md5Helper;

        public UserHandler(IUserInfoAccess userInfoAccess, IUtil util, IMd5Helper md5Helper)
        {
            this.md5Helper = md5Helper;
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
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
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
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var result = userInfoAccess.GetUserInfo(tokens.UserId);
            if (result == null)
                throw new TransactionException("01", "用户信息不存在");
            var userInfo = new UserInfo
            {
                Name = result.Name,
                Gender = result.Sex,
                Birth = result.Birthday.ToLocalTime(),
                Telphone = result.Phone,
                Address = result.Address,
                Origin = result.Origin,
                OriginAddress = result.OriginAddress,
                Ethnic = result.Ethnic,
                Branch = result.BranchId,
                Department = result.Dept,
                Education = result.Education,
                School = result.School,
                IdCard = result.IdNumber,
                Employer = result.Company,
                PrepPartyDate = result.PreMemberTime,
                FormalPartyDat = result.MemberTime,
                PartyType = result.MemberType,
                Provice = result.Provice,
                City = result.City,
                District = result.District,
                PhotoUrl = result.PhotoUrl
            };
            response.ResultUserInfo = userInfo;
            return response;
        }

        public LoginResponse Login(LoginRequest request)
        {
            var response = new LoginResponse();
            var guid = Guid.NewGuid().ToString("N");
            var result = userInfoAccess.GetLogin(request.Phone);
            if (result != null)
            {
                var password = md5Helper.Md5Encypt(request.UserPwd);
                if (!result.Password.Equals(password))
                {
                    throw new TransactionException(MessageCode.UserNameOrPasswordError,
                        MessageKey.UserNameOrPasswordError);
                }
                //登录新增Token
                var userToken = new TbUserToken()
                {
                    OrgId = result.OrgId,
                    BranchId = result.BranchId,
                    UserId = result.UserId,
                    Token = guid,
                    CreateTime = DateTime.Now,
                    ExpireTime = DateTime.Now.AddMinutes(30),
                    TokenType = 0
                };
                var tokenInfo = userInfoAccess.InsertToken(userToken);
                if (tokenInfo > 0)
                {
                    response.Token = guid;
                }
                else
                {
                    throw new TransactionException(MessageCode.LoginFail, MessageKey.LoginFail);
                }
                response.UserId = result.UserId;
                response.IsInternalUser = result.IsCustomer == 0;
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
            if (request.AppId <= 0)
            {
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            }
            if (!util.CheckAppId(request.AppId))
            {
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            }

            if (string.IsNullOrEmpty(request.Phone) || request.Phone.Length != 11 || string.IsNullOrEmpty(request.UserPwd))
            {
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            }

            var login = userInfoAccess.GetLogin(request.Phone);
            if (login != null)
            {
                throw new TransactionException(MessageCode.ExistsPhone, MessageKey.ExistsPhone);
            }
                
            var userInfo = new TbUserInfo()
            {
                Phone = request.Phone,
                Password = md5Helper.Md5Encypt(request.UserPwd),
                CreateTime = DateTime.Now,
                IsCustomer = 0,
                OrgId = request.AppId,
                BranchId = 0,
                Score = 0,
                EditTime = DateTime.Now,
                Status = 0,
                IsLeader = 0,
                Name = request.Phone.MaskPhone(),
            };
            var codeInfo = new TbValidCode() { Phone = request.Phone, ValidCode = request.ValidCode, Seq = request.Seq };
            var resultCode = userInfoAccess.GetValidCode(codeInfo);
            if (resultCode == null)
            {
                throw new TransactionException(MessageCode.InvalidCode, MessageKey.InvalidCode);
            }
            var result = userInfoAccess.CreateUser(userInfo);
            return response;
        }

        public GetUserRelationshipResponse GetUserRelationship(GetUserRelationshipRequest request)
        {
            var response = new GetUserRelationshipResponse();
            var userId = request.UserId;
            var superiorUserList = userInfoAccess.GetRelationship(userId, false);
            var lowerUserList = userInfoAccess.GetRelationship(userId, true);
            var userRelationship = new List<UserRelationship>()
            {

            };
            lowerUserList.ForEach(x =>
            {
                userRelationship.Add(new UserRelationship()
                {
                    Name = x.NickName,
                    UserId = x.SubUserId,
                    IsLeader = false
                });
            });
            superiorUserList.ForEach(x =>
            {
                userRelationship.Add(new UserRelationship()
                {
                    Name = x.NickName,
                    UserId = x.UserId,
                    IsLeader = true
                });
            });

            response.UserList = userRelationship;
            return response;
        }
    }
}