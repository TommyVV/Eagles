using Eagles.Base;
using Eagles.Application.Model.User.Login;
using Eagles.Application.Model.User.EditUser;
using Eagles.Application.Model.User.Register;
using Eagles.Application.Model.User.BranchUser;
using Eagles.Application.Model.User.EditUserNoticeIsRead;
using Eagles.Application.Model.User.GetUserInfo;
using Eagles.Application.Model.User.GetUserNotice;
using Eagles.Application.Model.User.GetUserRelationship;

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

        /// <summary>
        /// 更新用户通知为已读
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        EditUserNoticeIsReadResponse EditUserNoticeIsRead(EditUserNoticeIsReadRequest request);

        /// <summary>
        /// 用户信息查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetUserInfoResponse GetUserInfo(GetUserInfoRequest request);

        /// <summary>
        /// 用户通知查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetUserNoticeResponse GetUserNotice(GetUserNoticeRequest request);

        /// <summary>
        /// 用户上下级查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetUserRelationshipResponse GetUserRelationship(GetUserRelationshipRequest request);

        /// <summary>
        /// 查询支部用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetBranchUserResponse GetBranchUser(GetBranchUserRequest request);
    }
}