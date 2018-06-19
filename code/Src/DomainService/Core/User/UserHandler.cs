using Eagles.Base.DataBase;
using Eagles.Base.DesEncrypt;
using Eagles.Interface.Core.User;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.Core.DataBase.UserInfo;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.AppModel.User.Login;
using Eagles.Application.Model.AppModel.User.Register;
using Eagles.Application.Model.AppModel.User.EditUser;
using Eagles.Application.Model.AppModel.User.GetUserInfo;
using DomainModel = Eagles.DomainService.Model;

namespace Eagles.DomainService.Core.User
{
    public class UserHandler : IUserHandler
    {
        private readonly IUtil util;
        private readonly IDbManager dbManager;
        private readonly IDesEncrypt desEncrypt;
        private readonly IUserInfoAccess userInfoAccess;

        public UserHandler(IUserInfoAccess userInfoAccess, IUtil util, IDesEncrypt desEncrypt)
        {
            this.desEncrypt = desEncrypt;
            this.userInfoAccess = userInfoAccess;
            this.util = util;
        }

        public EditUserResponse EditUser(EditUserRequest request)
        {
            var response = new EditUserResponse();
            var reqUserInfo = request.RequestUserInfo;
            var result = userInfoAccess.EditUser(reqUserInfo);
            if (result > 0)
            {
                response.ErrorCode = "00";
                response.Message = "修改成功";
            }
            else
            {
                response.ErrorCode = "96";
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
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
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
            var userId = request.UserId;
            var pwd = request.UserPwd;
            var result = userInfoAccess.GetLogin(request.UserId);
            if (result != null)
            {
                var password1 = desEncrypt.Encrypt(result.Password);
                var password2 = desEncrypt.Encrypt(pwd);

                if (!password1.Equals(password2))
                {
                    response.ErrorCode = "96";
                    response.Message = "账户密码错误";
                    return response;
                }
                //登录新增Token
                response.Token = userInfoAccess.InsertToken(userId);
                //返回前端加密userId
                response.EncryptUserid = desEncrypt.Encrypt(userId.ToString());
                response.ErrorCode = "00";
                response.Message = "登录成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查无此账号";
            }
            return response;
        }

        public RegisterResponse Register(RegisterRequest request)
        {
            var response = new RegisterResponse();
            var phone = request.Phone;
            var code = request.SmsCode;
            var codeResult = dbManager.Query<DomainModel.Sms.ValidCode>("select Phone,ValidCode,ExpireTime FROM eagles.tb_validcode where phone = @phone ", phone);

            var result = dbManager.Query<DomainModel.User.UserInfo>("select UserId,Password from eagles.tb_user_info where Phone = @Phone ", phone);
            if (result != null && result.Count > 0)
            {
                response.ErrorCode = "96";
                response.Message = "账号已存在";
            }
            else
            {
                var expireTime = codeResult[0].ExpireTime;
                if (code != codeResult[0].Code)
                {
                    
                }
                
            }
            return response;
        }
        
    }
}