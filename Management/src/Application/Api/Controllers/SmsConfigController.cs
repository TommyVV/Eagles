using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.SMS.Request;
using Eagles.Application.Model.SMS.Response;
using Eagles.Base;
using Eagles.Interface.Core;

using Eagles.Application.Host.Common;
using Eagles.Application.Model.SMSOrg.Request;
using Eagles.Application.Model.SMSOrg.Response;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class SMSController : ApiController
    {
        private readonly ISmsConfigHandler _SmsHandler;

        public SMSController(ISmsConfigHandler testHandler)
        {
            this._SmsHandler = testHandler;
        }



        /// <summary>
        /// 编辑 短信配置
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> EditSMS(EditSMSRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) =>_SmsHandler.EditSMS(requset));
        }

        /// <summary>
        /// 删除 短信配置
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> RemoveSMS(RemoveSMSRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) =>_SmsHandler.RemoveSMS(requset));
        }

        /// <summary>
        /// 短信配置 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetSMSDetailResponse> GetSMSDetail(GetSMSDetailRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) =>_SmsHandler.GetSMSDetail(requset));
        }

        /// <summary>
        /// 短信配置 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetSMSResponse> GetSMS(GetSMSRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) =>_SmsHandler.SMS(requset));
        }








        /// <summary>
        /// 创建/修改短信配置。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseFormat<bool> EditOrgSmsConfig(EditSMSOrgRequset request)
        {

            return ApiActuator.Runing(request, (requset1) => _SmsHandler.EditOrgSmsConfig(request));
            //todo
            //return null;
        }

        ///// <summary>
        ///// 删除 短信配置
        ///// </summary>
        ///// <param name="requset"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ResponseFormat<bool> RemoveOrgSmsConfig(RemoveSMSOrgRequest requset)
        //{
        //    return ApiActuator.Runing(requset, (requset1) => _SmsHandler.RemoveOrgSmsConfig(requset));
        //}

        /// <summary>
        /// 短信配置 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetSMSOrgDetailResponse> GetSMSOrgDetail(GetSMSOrgDetailRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _SmsHandler.GetSMSOrgDetail(requset));
        }

        /// <summary>
        /// 短信配置 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetSMSOrgResponse> GetSMSOrg(GetSMSOrgRequest requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _SmsHandler.GetSMSOrg(requset));
        }

     

        
    }
}