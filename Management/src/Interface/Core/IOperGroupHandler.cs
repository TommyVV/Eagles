using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.AuthorityGroup.Requset;
using Eagles.Application.Model.AuthorityGroup.Response;
using Eagles.Application.Model.AuthorityGroupSetUp.Requset;
using Eagles.Application.Model.AuthorityGroupSetUp.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface IOperGroupHandler : IInterfaceBase
    {
        /// <summary>
        /// 编辑 管理员群组
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        ResponseBase EditOperGroup(EditAuthorityGroupRequset requset);

        /// <summary>
        /// 删除 管理员群组
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        ResponseBase RemoveOperGroup(RemoveAuthorityGroupInfoRequset requset);

        /// <summary>
        /// 管理员群组 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetAuthorityGroupDetailResponse GetOperGroupDetail(GetAuthorityGroupDetailRequset requset);

        /// <summary>
        /// 管理员群组 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetAuthorityGroupResponse GetOperGroupList(GetAuthorityGroupRequset requset);


        /// <summary>
        /// 编辑操作员菜单权限组
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        ResponseBase EditAuthorityGroupSetUp(EditAuthorityGroupSetUpRequset requset);

        /// <summary>
        /// 获得操作员菜单权限组
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetAuthorityGroupSetUpResponse GetAuthorityGroupSetUp(GetAuthorityGroupSetUpRequest requset);

    }
}
