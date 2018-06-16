using System;
using Eagles.Base.DataBase;
using Eagles.Base.DesEncrypt;
using Eagles.Interface.Core.User;
using Eagles.Application.Model.Curd.User.Login;
using Eagles.Application.Model.Curd.User.Register;
using Eagles.Application.Model.Curd.User.EditUser;
using Eagles.Application.Model.Curd.User.GetUserInfo;
using DomainModel = Eagles.DomainService.Model;

namespace Eagles.DomainService.Core.User
{
    public class UserHandler : IUserHandler
    {
        private readonly IDbManager dbManager;
        private readonly IDesEncrypt desEncrypt;

        public EditUserResponse EditUser(EditUserRequest request)
        {
            var user = request.RequestUserInfo;
            
            throw new NotImplementedException();
        }

        public GetUserInfoResponse GetUserInfo(GetUserInfoRequest request)
        {
            var response = new GetUserInfoResponse();
            //request.Token


            return response;
        }

        public LoginResponse Login(LoginRequest request)
        {
            var response = new LoginResponse();
            var userId = request.UserId;
            var pwd = request.UserPwd;
            var result = dbManager.Query<DomainModel.User.UserInfo>("select UserId,Password from eagles.tb_user_info where UserId = @UserId ", userId);
            if (result != null && result.Count > 0)
            {
                var password1 = desEncrypt.Encrypt(result[0].Password);
                var password2 = desEncrypt.Encrypt(pwd);

                if (!password1.Equals(password2))
                {
                    response.ErrorCode = "96";
                    response.Message = "账户密码错误";
                }
                //登录新增Token
                InsertToken(userId);
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

        private void InsertToken(int userId)
        {
            var token = Guid.NewGuid().ToString();
            var result = dbManager.Excuted(@"insert into eagles.tb_user_token (UserId,Token,CreateTime,ExpireTime,TokenType) value (@UserId,@Token,@CreateTime,@ExpireTime,@TokenType)",
                new object[] {userId, token, DateTime.Now, DateTime.Now.AddMinutes(30)});
            if (result > 0)
            {
                
            }
        }
    }
}