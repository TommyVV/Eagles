using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Eagles.Application.Model;
using Eagles.Application.Model.Enums;
using Eagles.Application.Model.PartyMember.Model;
using Eagles.Application.Model.PartyMember.Requset;
using Eagles.Application.Model.PartyMember.Response;
using Eagles.Base;
using Eagles.Base.Cache;
using Eagles.Base.Configuration;
using Eagles.Base.Md5Helper;
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

        private readonly IMd5Helper md5Helper;
        private readonly ICacheHelper cacheHelper;

        public UserHandler(IPartyMemberDataAccess dataAccess, IOrganizationDataAccess orgdataAccess, IConfigurationManager configurationManager, ICacheHelper cacheHelper, IMd5Helper md5Helper)
        {
            this.dataAccess = dataAccess;
            OrgdataAccess = orgdataAccess;
            this.configurationManager = configurationManager;
            this.cacheHelper = cacheHelper;
            this.md5Helper = md5Helper;
        }

        public GetPartyMemberResponse GetPartyMemberList(GetPartyMemberRequest request)
        {

            var response = new GetPartyMemberResponse
            {
            };
            //  //得到试卷 + 习题的关系
            List<TbUserInfo> list = dataAccess.GetUserInfoList(request, out int totalCount);

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");

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

            };
            TbUserInfo detail = dataAccess.GetUserInfoDetail(request);

            if (detail == null) throw new TransactionException("M01", "无业务数据");

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
                //  Nation = detail.Ethnic,
                NativePlace = detail.Origin,
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
                MemberStatus = detail.MemberStatus,
                Nation = detail.Ethnic
            };
            return response;
        }

        public bool RemoveUserInfoDetails(RemoveUserInfoDetailsRequest request)
        {

            return dataAccess.RemoveUserInfo(request) > 0;


        }

        public bool EditUserInfoDetails(EditUserInfoDetailsRequest request)
        {

            var tokenInfo = cacheHelper.GetData<TbUserToken>(request.Token);
            TbUserInfo mod;
            var password = md5Helper.Md5Encypt(request.Info.Password);
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
                    //Ethnic = request.Info.Nation,
                    Origin = request.Info.NativePlace,
                    OrgId = tokenInfo.OrgId,
                    NickPhotoUrl = request.Info.Picture,
                    Title = request.Info.Position,
                    Sex = request.Info.Sex,
                    IsCustomer = 1,
                    BranchId = tokenInfo.BranchId,
                    City = request.Info.City,
                    //  CreateTime = DateTime.Now,
                    Dept = request.Info.Dept,
                    District = request.Info.District,
                    EditTime = DateTime.Now,
                    MemberStatus = request.Info.MemberStatus,
                    MemberType = request.Info.UserStatus,
                    OperId = tokenInfo.UserId,
                    //Password = password,
                    PhotoUrl = request.Info.PhotoUrl,
                    Provice = request.Info.Provice,
                    Status = request.Info.Status,
                    Ethnic = request.Info.Nation,
                    IsLeader = 0,
                    Score = 0,
                };

                return dataAccess.EditUserInfo(mod) > 0;


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
                    //Ethnic = request.Info.Nation,
                    Origin = request.Info.NativePlace,
                    OrgId = tokenInfo.OrgId,
                    NickPhotoUrl = request.Info.Picture,
                    Title = request.Info.Position,
                    Sex = request.Info.Sex,
                    IsCustomer = 1,
                    BranchId = tokenInfo.BranchId,
                    City = request.Info.City,
                    CreateTime = DateTime.Now,
                    Dept = request.Info.Dept,
                    District = request.Info.District,
                    EditTime = DateTime.Now,
                    MemberStatus = request.Info.MemberStatus,
                    MemberType = request.Info.UserStatus,
                    OperId = tokenInfo.UserId,
                    Password = password,
                    PhotoUrl = request.Info.PhotoUrl,
                    Provice = request.Info.Provice,
                    Status = request.Info.Status,
                    Ethnic = request.Info.Nation,
                    IsLeader = 0,
                    Score = 0,
                    
                };

                return dataAccess.CreateUserInfo(mod) > 0;
            }
        }

        public GetAuthorityUserSetUpResponse GetAuthorityUserSetUp(GetAuthorityUserSetUpRequset requset)
        {
            var response = new GetAuthorityUserSetUpResponse
            {

            };
            //  用户列表
            List<TbUserInfo> list = dataAccess.GetUserInfoList(requset, out int totalCount);

            //获取用户下级权限
            List<TbUserRelationship> userSetUp = dataAccess.GetAuthorityUserSetUp(requset.UserId);

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");

            //机构信息
            var orgList = OrgdataAccess.GetOrganizationList(list.Select(x => x.OrgId).ToList());

            response.TotalCount = totalCount;

            var userChecklist = list.Select(x => new UserInfoCheck
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

            response.UserId = userChecklist.Select(x => x.UserId).ToList();
            return response;
        }

        public bool CreateAuthorityUserSetUp(CreateAuthorityUserSetUp requset)
        {


            List<TbUserRelationship> list;


            list = requset.UserIds.Select(x => new TbUserRelationship
            {
                // OrgId = requset.OrgId,
                UserId = requset.UserId,
                SubUserId = x
            }).ToList();

            return dataAccess.CreateAuthorityUserSetUp(list) > 0;


        }

        public bool RemoveAuthorityUserSetUp(RemoveAuthorityUserSetUp requset)
        {


            List<TbUserRelationship> list;


            list = requset.UserIds.Select(x => new TbUserRelationship
            {
                //  OrgId = requset.OrgId,
                UserId = requset.UserId,
                SubUserId = x
            }).ToList();

            return dataAccess.RemoveAuthorityUserSetUp(list) > 0;


        }

        public ImportUserResponse BatchImportUser(ImportUserRequest request)
        {

            var response = new ImportUserResponse { UserList = new List<ImportUser>() };
            Regex rx = new Regex(@"^0{0,1}(13[4-9]|15[7-9]|15[0-2]|18[7-8])[0-9]{8}$");
            var userinfo = new List<TbUserInfo>();


            foreach (var md in request.UserList)
            {

                if (!(md.MemberType == 1 || md.MemberType == 0))
                {
                    md.ImportStatus = false;
                    md.ErrorReason = "党员类型错误！";
                    response.UserList.Add(md);
                    break;
                }

                if (!rx.IsMatch(md.Phone))
                {
                    md.ImportStatus = false;
                    md.ErrorReason = "手机号格式错误！";
                    response.UserList.Add(md);
                    break;
                }

                userinfo.Add(new TbUserInfo
                {
                    Name = md.UserName,
                    Phone = md.Phone,
                    MemberType = md.MemberType,

                });


            }

            if (dataAccess.CreateUserInfo(userinfo)<=0)
            {
                response.UserList.AddRange(userinfo.Select(x => new ImportUser()
                {
                    ImportStatus = false,
                    ErrorReason = "添加数据库失败",
                    MemberType = x.MemberType,
                    Phone = x.Phone,
                    UserName = x.Name
                }));
            }

            return response;

            // throw new NotImplementedException();
        }
    }
}
