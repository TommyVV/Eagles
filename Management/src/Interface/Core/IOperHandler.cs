using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.Operator.Requset;
using Eagles.Application.Model.Operator.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface IOperHandler: IInterfaceBase
    {
        /// <summary>
        /// 编辑 管理员
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        bool EditOper(EditOperatorRequset requset);

        /// <summary>
        /// 删除 管理员
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        bool RemoveOper(RemoveOperatorRequset requset);

        /// <summary>
        /// 管理员 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetOperatorDetailResponse GetOperDetail(GetOperatorDetailRequset requset);

        /// <summary>
        /// 管理员 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetOperatorResponse GetOperList(GetOperatorRequset requset);
    }
}
