﻿using System;
using Eagles.Base;
using Eagles.Application.Model.User.EditUser;
using Eagles.Application.Model.User.GetUserInfo;
using Eagles.Application.Model.User.Login;
using Eagles.Application.Model.User.Register;

namespace Eagles.Interface.Core.User
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserHandler : IInterfaceBase
    {
        /// <summary>
        /// 用户信息修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        EditUserResponse EditUser(EditUserRequest request);

        /// <summary>
        /// 用户信息查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetUserInfoResponse GetUserInfo(GetUserInfoRequest request);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        LoginResponse Login(LoginRequest request);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RegisterResponse Register(RegisterRequest request);
    }
}