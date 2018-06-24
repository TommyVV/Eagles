using System;
using System.Collections.Generic;
using System.Linq;
using Eagles.Application.Model;
using Eagles.Application.Model.Enums;
using Eagles.Application.Model.PartyMember.Model;
using Eagles.Application.Model.PartyMember.Requset;
using Eagles.Application.Model.PartyMember.Response;
using Eagles.Base.Configuration;
using Eagles.DomainService.Model.Org;
using Eagles.DomainService.Model.User;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class UserHandler : IUserHandler
    {
        private readonly IPartyMemberDataAccess dataAccess;

        private readonly IOrganizationDataAccess OrgdataAccess;

        private readonly IConfigurationManager configurationManager;

        public PartyMemberHandler(IPartyMemberDataAccess dataAccess, IOrganizationDataAccess orgdataAccess, IConfigurationManager configurationManager)
        {
            this.dataAccess = dataAccess;
            OrgdataAccess = orgdataAccess;
            this.configurationManager = configurationManager;
        }

        public GetPartyMemberResponse GetPartyMemberList(GetPartyMemberRequest request)
        {

            var response = new GetPartyMemberResponse
            {
                ErrorCode = "00",
                IsSuccess = true,
                Message = "成功",
            };
            //  //得到试卷 + 习题的关系
            List<TbUserInfo> list = dataAccess.GetUserInfoList(request, out int totalCount);

            if (list.Count == 0) throw new Exception("无数据");

            List<TbOrgInfo> orgList = OrgdataAccess.GetOrganizationList(list.Select(x => x.OrgId).ToList());

            response.TotalCount = totalCount;
            response.List = list.Select(x => new Member
            {
                OrgName = orgList.First(o => o.OrgId == x.OrgId).OrgName,
                Phone = x.Phone,
                UserId = x.UserId,
                UserName = x.Name,
            }).ToList();

            return response;

        }

        public GetUserInfoDetailResponse GetUserInfoDetail(GetUserInfoDetailRequest request)
        {

            var response = new GetUserInfoDetailResponse
            {
                ErrorCode = "00",
                Message = "成功",
            };
            TbUserInfo detail = dataAccess.GetUserInfoDetail(request);

            if (detail == null) throw new Exception("无数据");

            List<TbOrgInfo> orgList = OrgdataAccess.GetOrganizationList(new List<int> { detail.OrgId });

            response.Info = new UserInfoDetails
            {
                OrgName = orgList.First(o => o.OrgId == detail.OrgId).OrgName,
                Phone = detail.Phone,
                UserId = detail.UserId,
                UserName = detail.Name,
                BeforJoinTime = detail.PreMemberTime,
                Birth = detail.Birthday,
                Company = detail.Company,
                DefaultAddress = detail.Address,
                Education = detail.Education,
                FamilyAddress = detail.OriginAddress,
                FormalJoinTime = detail.MemberTime,
                GraduateSchool = detail.School,
                IdCard = detail.IdNumber,
                // IsMoney=detail.
                Nation = detail.Ethnic,
                NativePlace = detail.Origin,
                OrgId = detail.OrgId,
                Picture = detail.NickPhotoUrl,
                Position = detail.Title,
                Sex = detail.Sex,
                BranchId = detail.BranchId,
                Status = detail.Status,
                Provice = detail.Provice,
                PhotoUrl = detail.PhotoUrl,
                UserStatus = detail.MemberType,
                City = detail.City,
                CreateTime = detail.CreateTime,
                Dept = detail.Dept,
                District = detail.Dept,
                IsCustomer = detail.IsCustomer,
                //    OperId=detail.OperId,
                EditTime = detail.EditTime,
                MemberStatus = detail.MemberStatus,

                //UserStatus=detail.IsCustomer
            };
            return response;
        }

        public ResponseBase RemoveUserInfoDetails(RemoveUserInfoDetailsRequest request)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };
            int result = dataAccess.RemoveUserInfo(request);

            if (result > 0)
            {
                response.IsSuccess = true;
            }

            return response;
        }

        public ResponseBase EditUserInfoDetails(EditUserInfoDetailsRequest request)
        {

            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };

            TbUserInfo mod;

            if (request.Info.UserId > 0)
            {
                mod = new TbUserInfo
                {
                    Phone = request.Info.Phone,
                    UserId = request.Info.UserId,
                    Name = request.Info.UserName,
                    PreMemberTime = request.Info.BeforJoinTime,
                    Birthday = request.Info.Birth,
                    Company = request.Info.Company,
                    Address = request.Info.DefaultAddress,
                    Education = request.Info.Education,
                    OriginAddress = request.Info.FamilyAddress,
                    MemberTime = request.Info.FormalJoinTime,
                    School = request.Info.GraduateSchool,
                    IdNumber = request.Info.IdCard,
                    // IsMoney   ,                                                                  
                    Ethnic = request.Info.Nation,
                    Origin = request.Info.NativePlace,
                    OrgId = request.Info.OrgId,
                    NickPhotoUrl = request.Info.Picture,
                    Title = request.Info.Position,
                    Sex = request.Info.Sex,
                    IsCustomer = 1,
                    BranchId = 0,
                    City = request.Info.City,
                    //  CreateTime = DateTime.Now,
                    Dept = request.Info.Dept,
                    District = request.Info.District,
                    EditTime = DateTime.Now,
                    MemberStatus = request.Info.MemberStatus,
                    MemberType = request.Info.UserStatus,
                    OperId = 0,
                    Password = request.Info.Password,
                    PhotoUrl = request.Info.PhotoUrl,
                    Provice = request.Info.Provice,
                    Status = request.Info.Status,
                };

                int result = dataAccess.EditUserInfo(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
            }
            else
            {
                mod = new TbUserInfo
                {

                    Phone = request.Info.Phone,
                    UserId = request.Info.UserId,
                    Name = request.Info.UserName,
                    PreMemberTime = request.Info.BeforJoinTime,
                    Birthday = request.Info.Birth,
                    Company = request.Info.Company,
                    Address = request.Info.DefaultAddress,
                    Education = request.Info.Education,
                    OriginAddress = request.Info.FamilyAddress,
                    MemberTime = request.Info.FormalJoinTime,
                    School = request.Info.GraduateSchool,
                    IdNumber = request.Info.IdCard,
                    // IsMoney   ,                                                                  
                    Ethnic = request.Info.Nation,
                    Origin = request.Info.NativePlace,
                    OrgId = request.Info.OrgId,
                    NickPhotoUrl = request.Info.Picture,
                    Title = request.Info.Position,
                    Sex = request.Info.Sex,
                    IsCustomer = 1,
                    BranchId = 0,
                    City = request.Info.City,
                    CreateTime = DateTime.Now,
                    Dept = request.Info.Dept,
                    District = request.Info.District,
                    //   EditTime = DateTime.Now,
                    MemberStatus = request.Info.MemberStatus,
                    MemberType = request.Info.UserStatus,
                    OperId = 0,
                    Password = request.Info.Password,
                    PhotoUrl = request.Info.PhotoUrl,
                    Provice = request.Info.Provice,
                    Status = request.Info.Status,
                };

                int result = dataAccess.CreateUserInfo(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
            }

            return response;

        }

        public GetAuthorityUserSetUpResponse GetAuthorityUserSetUp(GetAuthorityUserSetUpRequset requset)
        {
            var response = new GetAuthorityUserSetUpResponse
            {
                ErrorCode = "00",
                IsSuccess = true,
                Message = "成功",
            };
            //  用户列表
            List<TbUserInfo> list = dataAccess.GetUserInfoList(requset, out int totalCount);

            //获取用户下级权限
            List<TbUserRelationship> userSetUp = dataAccess.GetAuthorityUserSetUp(requset.UserId);

            if (list.Count == 0) throw new Exception("无数据");

            //机构信息
            var orgList = OrgdataAccess.GetOrganizationList(list.Select(x => x.OrgId).ToList());

            response.TotalCount = totalCount;

           var userChecklist =  list.Select(x => new UserInfoCheck
            {
                OrgName = orgList.First(o => o.OrgId == x.OrgId).OrgName,
                Phone = x.Phone,
                UserId = x.UserId,
                UserName = x.Name,
                IsCheck = userSetUp.Any(up => up.SubUserId == x.UserId)
            }).ToList();

            if (requset.IsRelation == IsRelation.关联)
            {
                userChecklist = userChecklist.Where(x => x.IsCheck).ToList();
            }
            else if (requset.IsRelation == IsRelation.未关联)
            {
                userChecklist = userChecklist.Where(x => !x.IsCheck).ToList();
            }

            response.List = userChecklist;
            return response;
        }

        public ResponseBase CreateAuthorityUserSetUp(CreateAuthorityUserSetUp requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };

            List<TbUserRelationship> list;


            list = requset.UserIds.Select(x => new TbUserRelationship
            {
                OrgId = requset.OrgId,
                UserId = requset.UserId,
                SubUserId = x
            }).ToList();

            int result = dataAccess.CreateAuthorityUserSetUp(list);

            if (result > 0)
            {
                response.IsSuccess = true;
            }

            return response;
        }

        public ResponseBase RemoveAuthorityUserSetUp(RemoveAuthorityUserSetUp requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };

            List<TbUserRelationship> list;


            list = requset.UserIds.Select(x => new TbUserRelationship
            {
                OrgId = requset.OrgId,
                UserId = requset.UserId,
                SubUserId = x
            }).ToList();

            int result = dataAccess.RemoveAuthorityUserSetUp(list);

            if (result > 0)
            {
                response.IsSuccess = true;
            }

            return response;
        }
    }
}
