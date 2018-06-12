using System;
using Eagles.Interface.Core.User;
using Eagles.Application.Model.Curd.User.Login;
using Eagles.Application.Model.Curd.User.Register;
using Eagles.Application.Model.Curd.User.EditUser;
using Eagles.Application.Model.Curd.User.GetUserInfo;

namespace Eagles.DomainService.Core
{
    public class UserHandler : IUserHandler
    {
        public EditUserResponse EditUser(EditUserRequest request)
        {
            var user = request.RequestUserInfo;
            
            throw new NotImplementedException();
        }

        public GetUserInfoResponse GetUserInfo(GetUserInfoRequest request)
        {
            throw new NotImplementedException();
        }

        public LoginResponse Login(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public RegisterResponse Register(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}