using System;
using System.Linq;
using System.Collections.Generic;
using Eagles.Base;
using Eagles.Base.Md5Helper;
using Eagles.Interface.Core.User;
using Eagles.Interface.Configuration;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.DataAccess.UserInfo;
using Eagles.DomainService.Model.Sms;
using Eagles.DomainService.Model.User;
using Eagles.DomainService.Core.Utility;
using Eagles.Application.Model;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.User.Login;
using Eagles.Application.Model.User.Register;
using Eagles.Application.Model.User.EditUser;
using Eagles.Application.Model.User.BranchUser;
using Eagles.Application.Model.User.EditUserNoticeIsRead;
using Eagles.Application.Model.User.EditUserPwd;
using Eagles.Application.Model.User.GetUserInfo;
using Eagles.Application.Model.User.GetUserNotice;
using Eagles.Application.Model.User.GetUserRelationship;

namespace Eagles.DomainService.Core.User
{
    public class UserHandler : IUserHandler
    {
        private readonly IUserInfoAccess userInfoAccess;
        private readonly IUtil util;
        private readonly IMd5Helper md5Helper;
        private readonly IEaglesConfig configuration;

        public UserHandler(IUserInfoAccess userInfoAccess, IUtil util, IMd5Helper md5Helper, IEaglesConfig configuration)
        {
            this.md5Helper = md5Helper;
            this.userInfoAccess = userInfoAccess;
            this.util = util;
            this.configuration = configuration;
        }

        public EditUserResponse EditUser(EditUserRequest request)
        {
            var response = new EditUserResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var reqUserInfo = request.RequestUserInfo;
            var userInfo = new TbUserInfo()
            {
                UserId = tokens.UserId,
                PhotoUrl = reqUserInfo.PhotoUrl,
                Name = reqUserInfo.Name,
                Sex = reqUserInfo.Gender,
                Birthday = reqUserInfo.Birth,
                Address = reqUserInfo.Address,
                OriginAddress = reqUserInfo.OriginAddress,
                Ethnic = reqUserInfo.Ethnic,
                Dept = reqUserInfo.Department,
                Company = reqUserInfo.Employer,
                Origin = reqUserInfo.Origin
            };
            var result = userInfoAccess.EditUser(userInfo);
            if (result <= 0)
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            return response;
        }

        public EditUserPwdResponse EditUserPwd(EditUserPwdRequest request)
        {
            var response = new EditUserPwdResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var result = userInfoAccess.GetLogin(request.Phone);
            if (result == null)
                throw new TransactionException(MessageCode.UserNotExists, MessageKey.UserNotExists);
            var password = md5Helper.Md5Encypt(request.UserPwd);
            if (!result.Password.Equals(password))
                throw new TransactionException(MessageCode.UserNameOrPasswordError, MessageKey.UserNameOrPasswordError);
            var newPwd = md5Helper.Md5Encypt(request.NewPwd);
            var userInfo = new TbUserInfo()
            {
                Phone = request.Phone,
                Password = newPwd
            };
            userInfoAccess.EditUserPwd(userInfo);
            return response;
        }

        public LoginResponse Login(LoginRequest request)
        {
            var response = new LoginResponse();
            var tokenExp = configuration.EaglesConfiguration.TokenExpTime;
            var now = DateTime.Now;
            var expireTime = now.AddMinutes(tokenExp);
            var guid = Guid.NewGuid().ToString("N");
            var result = userInfoAccess.GetLogin(request.Phone);
            if (result == null)
                throw new TransactionException(MessageCode.UserNotExists, MessageKey.UserNotExists);
            var password = md5Helper.Md5Encypt(request.UserPwd);
            if (!result.Password.Equals(password))
                throw new TransactionException(MessageCode.UserNameOrPasswordError, MessageKey.UserNameOrPasswordError);
            //登录新增Token
            var userToken = new TbUserToken()
            {
                OrgId = result.OrgId,
                BranchId = result.BranchId,
                UserId = result.UserId,
                Token = guid,
                CreateTime = now,
                ExpireTime = expireTime,
                TokenType = 0
            };
            var tokenInfo = userInfoAccess.InsertToken(userToken);
            if (tokenInfo > 0)
                response.Token = guid;
            else
                throw new TransactionException(MessageCode.LoginFail, MessageKey.LoginFail);
            response.UserId = result.UserId;
            response.IsInternalUser = result.IsCustomer;
            response.TokenExpTime = expireTime.ToString("yyyy-MM-dd HH:mm:ss");
            
            return response;
        }

        public RegisterResponse Register(RegisterRequest request)
        {
            var response = new RegisterResponse();
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (string.IsNullOrEmpty(request.Phone) || request.Phone.Length != 11 || string.IsNullOrEmpty(request.UserPwd))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var login = userInfoAccess.GetLogin(request.Phone);
            if (login != null)
                throw new TransactionException(MessageCode.ExistsPhone, MessageKey.ExistsPhone);
            var userInfo = new TbUserInfo()
            {
                OrgId = request.AppId,
                Name = request.Phone.MaskPhone(),
                Phone = request.Phone,
                Password = md5Helper.Md5Encypt(request.UserPwd),
                CreateTime = DateTime.Now,
                IsCustomer = 0,
                BranchId = 0,
                Score = 0,
                EditTime = DateTime.Now,
                Status = 0
                //IsLeader = 0,
            };
            var codeInfo = new TbValidCode() { Phone = request.Phone, ValidCode = request.ValidCode, Seq = request.Seq };
            var resultCode = userInfoAccess.GetValidCode(codeInfo);
            if (resultCode == null)
                throw new TransactionException(MessageCode.InvalidCode, MessageKey.InvalidCode);            
            if (resultCode.ExpireTime < DateTime.Now)
                throw new TransactionException(MessageCode.ExpireValidateCode, MessageKey.ExpireValidateCode);            
            var result = userInfoAccess.CreateUser(userInfo);
            return response;
        }

        public EditUserNoticeIsReadResponse EditUserNoticeIsRead(EditUserNoticeIsReadRequest request)
        {
            var response = new EditUserNoticeIsReadResponse();
            if (request.NewsId < 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var result = userInfoAccess.EditUserNoticeIsRead(request.NewsId);
            if (result <= 0)
                return response;
            else
                return new EditUserNoticeIsReadResponse() {Code = "96", Message = "更新失败"};
        }

        public GetUserInfoResponse GetUserInfo(GetUserInfoRequest request)
        {
            var response = new GetUserInfoResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidParameter);
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var result = userInfoAccess.GetUserInfo(tokens.UserId);
            if (result == null)
                throw new TransactionException("01", "用户信息不存在");
            var userInfo = new ResUser
            {
                UserId = result.UserId,
                Name = result.Name,
                Gender = result.Sex,
                Birth = result.Birthday.ToString("yyyy-MM-dd"),
                Telphone = result.Phone,
                Address = result.Address,
                Origin = result.Origin,
                OriginAddress = result.OriginAddress,
                Ethnic = result.Ethnic,
                Branch = result.BranchName,
                Department = result.Dept,
                Education = result.Education,
                School = result.School,
                IdCard = result.IdNumber,
                Employer = result.Company,
                PrepPartyDate = result.PreMemberTime.ToString("yyyy-MM-dd"),
                FormalPartyDat = result.MemberTime.ToString("yyyy-MM-dd"),
                PartyType = result.MemberType == 0 ? "党员" : "预备党员",
                Provice = result.Provice,
                City = result.City,
                District = result.District,
                PhotoUrl = result.PhotoUrl,
                Score = result.Score,
                MyOrganization = result.OrgName
            };
            response.ResultUserInfo = userInfo;
            return response;
        }

        public GetUserNoticeResponse GetUserNotice(GetUserNoticeRequest request)
        {
            var response = new GetUserNoticeResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidParameter);
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var result = userInfoAccess.GetUserNotice(tokens.UserId, request.AppId, request.PageIndex, request.PageSize);
            if (result != null && result.Count > 0)
            {
                response.NoticeList = result?.Select(x => new UserNotice
                {
                    NewsId = x.NewsId,
                    Title = x.Title,
                    Content = x.Content,
                    IsRead = x.IsRead,
                    CreateTime = x.CreateTime.ToString("yyyy-MM-dd"),
                    UserId = x.UserId,
                    FromUser = x.FromUser,
                    TargetUrl = x.TargetUrl
                }).ToList();
            }
            else
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }

        public GetUserRelationshipResponse GetUserRelationship(GetUserRelationshipRequest request)
        {
            var response = new GetUserRelationshipResponse();
            var userId = request.UserId;
            var lowerUserList = userInfoAccess.GetRelationship(userId, true);
            var superiorUserList = userInfoAccess.GetRelationship(userId, false);
            var userRelationship = new List<UserRelationship>();
            switch (request.Type)
            {
                case 0:
                    lowerUserList.ForEach(x =>
                    {
                        userRelationship.Add(new UserRelationship()
                        {
                            Name = x.Name,
                            UserId = x.SubUserId,
                            IsLeader = false
                        });
                    });
                    superiorUserList.ForEach(x =>
                    {
                        userRelationship.Add(new UserRelationship()
                        {
                            Name = x.Name,
                            UserId = x.UserId,
                            IsLeader = true
                        });
                    });
                    response.UserList = userRelationship;
                    return response;
                case 1:
                    superiorUserList.ForEach(x =>
                    {
                        userRelationship.Add(new UserRelationship()
                        {
                            Name = x.Name,
                            UserId = x.UserId,
                            IsLeader = true
                        });
                    });
                    response.UserList = userRelationship;
                    return response;
                case 2:
                    lowerUserList.ForEach(x =>
                    {
                        userRelationship.Add(new UserRelationship()
                        {
                            Name = x.Name,
                            UserId = x.SubUserId,
                            IsLeader = false
                        });
                    });
                    response.UserList = userRelationship;
                    return response;
            }
            return response;
        }

        public GetBranchUserResponse GetBranchUser(GetBranchUserRequest request)
        {
            if (string.IsNullOrEmpty(request.Token))
            {
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            }

            if (request.AppId <= 0)
            {
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            }

            if (!util.CheckAppId(request.AppId))
            {
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            }

            var userInfo = util.GetUserId(request.Token, 0);
            if (userInfo == null)
            {
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            }
            var branchId = userInfo.BranchId;
            var result = userInfoAccess.GetBranchUser(branchId);
            var branchUser = result.Select(x => new BranchUser()
            {
                UserId = x.UserId,
                UserName = x.Name
            }).ToList();
            return new GetBranchUserResponse()
            {
                BranchUsers = branchUser
            };
        }
    }
}