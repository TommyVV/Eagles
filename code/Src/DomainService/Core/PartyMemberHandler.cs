using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.PartyMember.Model;
using Eagles.Application.Model.PartyMember.Requset;
using Eagles.Application.Model.PartyMember.Response;
using Eagles.Base.Configuration;
using Eagles.DomainService.Model.Org;
using Eagles.DomainService.Model.PartyMember;
using Eagles.Interface.Core;
using Eagles.Interface.Core.DataBase;

namespace Eagles.DomainService.Core
{
    public class PartyMemberHandler : IPartyMemberHandler
    {
        private readonly IPartyMemberDataAccess dataAccess;

        private readonly IOrganizationDataAccess OrgdataAccess;

        private readonly IConfigurationManager configurationManager;

        public GetPartyMemberResponse GetPartyMemberList(GetPartyMemberRequest request)
        {

            var response = new GetPartyMemberResponse
            {
                ErrorCode = "00",
                IsSuccess = true,
                Message = "成功",
            };
            //  //得到试卷 + 习题的关系
            List<TB_USER_INFO> list = dataAccess.GetUserInfoList(request, out int totalCount);

            if (list.Count == 0) throw new Exception("无数据");

            List<TB_ORG_INFO> orgList = OrgdataAccess.GetOrganizationList(list.Select(x => x.OrgId).ToList());

            response.TotalCount = totalCount;
            response.List = list.Select(x => new UserInfo
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
            TB_USER_INFO detail = dataAccess.GetUserInfoDetail(request);

            if (detail == null) throw new Exception("无数据");

            List<TB_ORG_INFO> orgList = OrgdataAccess.GetOrganizationList(new List<int> { detail.OrgId });

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
                Education = detail.Eduction,
                FamilyAddress = detail.OriginAddress,
                FormalJoinTime = detail.MemberTime,
                GraduateSchool = detail.School,
                IdCard = detail.IdNumber,
                // IsMoney=detail.
                Nation = detail.Ethinc,
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

            TB_USER_INFO mod;

            if (request.Info.UserId > 0)
            {
                mod = new TB_USER_INFO
                {
                    Phone = request.Info.Phone,
                    UserId = request.Info.UserId,
                    Name = request.Info.UserName,
                    PreMemberTime = request.Info.BeforJoinTime,
                    Birthday = request.Info.Birth,
                    Company = request.Info.Company,
                    Address = request.Info.DefaultAddress,
                    Eduction = request.Info.Education,
                    OriginAddress = request.Info.FamilyAddress,
                    MemberTime = request.Info.FormalJoinTime,
                    School = request.Info.GraduateSchool,
                    IdNumber = request.Info.IdCard,
                    // IsMoney   ,                                                                  
                    Ethinc = request.Info.Nation,
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
                mod = new TB_USER_INFO
                {

                    Phone = request.Info.Phone,
                    UserId = request.Info.UserId,
                    Name = request.Info.UserName,
                    PreMemberTime = request.Info.BeforJoinTime,
                    Birthday = request.Info.Birth,
                    Company = request.Info.Company,
                    Address = request.Info.DefaultAddress,
                    Eduction = request.Info.Education,
                    OriginAddress = request.Info.FamilyAddress,
                    MemberTime = request.Info.FormalJoinTime,
                    School = request.Info.GraduateSchool,
                    IdNumber = request.Info.IdCard,
                    // IsMoney   ,                                                                  
                    Ethinc = request.Info.Nation,
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
            //  //得到试卷 + 习题的关系
            List<TB_USER_INFO> list = dataAccess.GetUserInfoList(requset, out int totalCount);

            List<TB_USER_INFO> userSetUp = dataAccess.GetAuthorityUserSetUp(requset.UserId);

            if (list.Count == 0) throw new Exception("无数据");

           // List<TB_USER_RELATIONSHIP> orgList = OrgdataAccess.GetOrganizationList(list.Select(x => x.OrgId).ToList());

            response.TotalCount = totalCount;
            //response.List = list.Select(x => new UserInfoCheck
            //{
            //    OrgName = orgList.First(o => o.OrgId == x.OrgId).OrgName,
            //    Phone = x.Phone,
            //    UserId = x.UserId,
            //    UserName = x.Name,
            //    isCheck=userSetUp.Where(up=> up.c== orgList.)
            //}).ToList();
            return response;
        }

        public ResponseBase CreateAuthorityUserSetUp(CreateAuthorityUserSetUp requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };

            List<TB_USER_RELATIONSHIP> list;


            list = requset.UserIds.Select(x => new TB_USER_RELATIONSHIP
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

            List<TB_USER_RELATIONSHIP> list;


            list = requset.UserIds.Select(x => new TB_USER_RELATIONSHIP
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
