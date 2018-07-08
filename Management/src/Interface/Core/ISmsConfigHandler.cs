using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.SMS.Request;
using Eagles.Application.Model.SMS.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface ISmsConfigHandler : IInterfaceBase
    {
        /// <summary>
        /// 编辑短信配置
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        bool EditSMS(EditSMSRequset requset);

        /// <summary>
        /// 删除短信配置
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        bool RemoveSMS(RemoveSMSRequset requset);

        /// <summary>
        /// 短信配置列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetSMSResponse SMS(GetSMSRequset requset);
        /// <summary>
        /// 获取短信配置详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetSMSDetailResponse GetSMSDetail(GetSMSDetailRequset requset);
    }
}
