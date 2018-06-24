using System;
using System.Linq;
using System.Text;
using Eagles.Base.DataBase;
using Eagles.Interface.DataAccess.UserInfo;

namespace Ealges.DomianService.DataAccess.UserInfo
{
    public class UserDataAccess :IUserInfoAccess
    {
        private readonly IDbManager dbManager;

        public UserDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public int EditUser(Eagles.Application.Model.Common.UserInfo reqUserInfo)
        {
            var sql = new StringBuilder();
            sql.AppendFormat(@"update `eagles`.`tb_user_info` set Name=@Name, Sex=@Sex, Birthday=@Birthday,Phone=@Phone,Address=@Address,Origin=@Origin,OriginAddress=@OriginAddress,Ethnic=@Ethnic,BranchId=@BranchId,
Dept=@Dept,Education=@Education,School=@School,IdNumber=@IdNumber,Company=@Company,PreMemberTime=@PreMemberTime,MemberTime=@MemberTime,MemberType=@MemberType,Provice=@Provice,City=@City,District=@District,PhotoUrl=@PhotoUrl
where userId = @userId");
            var userId = reqUserInfo.UserId;
            var name = reqUserInfo.Name;
            var gender = reqUserInfo.Gender;
            var birth = reqUserInfo.Birth;
            var telphone = reqUserInfo.Telphone;
            var address = reqUserInfo.Address;
            var censusAddress = reqUserInfo.Address;
            var ethnic = reqUserInfo.Ethnic;
            var branch = reqUserInfo.Branch;
            var department = reqUserInfo.Department;
            var education = reqUserInfo.Education;
            var school = reqUserInfo.School;
            var idCard = reqUserInfo.IdCard;
            var employer = reqUserInfo.Employer;
            var origin = reqUserInfo.Origin;
            var prepPartyDate = reqUserInfo.PrepPartyDate;
            var formalPartyDat = reqUserInfo.FormalPartyDat;
            var partyType = reqUserInfo.PartyType;
            var provice = reqUserInfo.Provice;
            var city = reqUserInfo.City;
            var district = reqUserInfo.District;
            var photoUrl = reqUserInfo.PhotoUrl;
            return dbManager.Excuted(sql.ToString(),
                new
                {
                    Name = name,
                    Gender = gender,
                    Birth = birth,
                    Telphone = telphone,
                    Address = address,
                    CensusAddress = censusAddress,
                    Ethnic = ethnic,
                    Branch = branch,
                    Department = department,
                    Education = education,
                    School = school,
                    IdCard = idCard,
                    Employer = employer,
                    Origin = origin,
                    PrepPartyDate = prepPartyDate,
                    FormalPartyDat = formalPartyDat,
                    PartyType = partyType,
                    Provice = provice,
                    City = city,
                    District = district,
                    PhotoUrl = photoUrl,
                    UserId = userId
                });
        }

        public Eagles.DomainService.Model.User.TbUserInfo GetUserInfo(int userId)
        {
            var userInfo = dbManager.Query<Eagles.DomainService.Model.User.TbUserInfo>(
                @"SELECT OrgId,BranchId,UserId,Password,Name,Sex,Ethinc,Birthday,Origin,OriginAddress,Phone,IdNumber,Eduction,
School,Provice,City,District,Address,Company,Dept,Title,PreMemberTime,MemberTime,MemberType,Status,MemberStatus,
PhotoUrl,NickPhotoUrl,CreateTime,EditTime,OperId,IsCustomer FROM eagles.tb_user_info where UserId=@UserId", new { UserId = userId});
            if (userInfo != null && userInfo.Any())
                return userInfo.FirstOrDefault();
            return null;
        }

        public Eagles.DomainService.Model.User.TbUserInfo GetLogin(int userId)
        {
            var userInfo = dbManager.Query<Eagles.DomainService.Model.User.TbUserInfo>(
                "select UserId,Password from eagles.tb_user_info where UserId = @UserId ", new {UserId = userId});
            if (userInfo != null && userInfo.Any())
                return userInfo.FirstOrDefault();
            return null;
        }

        public string InsertToken(int userId)
        {
            var token = Guid.NewGuid().ToString();
            dbManager.Excuted(@"insert into eagles.tb_user_token (UserId,Token,CreateTime,ExpireTime,TokenType) value (@UserId,@Token,@CreateTime,@ExpireTime,@TokenType)",
            new
            {
                UserId = new[] {userId},
                Token = new[] {token},
                CreateTime = new[] {DateTime.Now},
                ExpireTime = new[] {DateTime.Now.AddMinutes(30)},
                TokenType = new[] {"0"}
            });
            return token;
        }
    }
}