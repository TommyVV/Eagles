﻿using System.Web.Http;
using Eagles.Application.Host.Common;
using Eagles.Application.Model;
using Eagles.Application.Model.Audit.Model;
using Eagles.Application.Model.Audit.Requset;
using Eagles.Application.Model.Audit.Response;
using Eagles.Base;
using Eagles.Interface.Core;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditController : ApiController
    {
        private readonly IAuditHandler _AuditHandler;

        public AuditController(IAuditHandler testHandler)
        {
            this._AuditHandler = testHandler;
        }



        /// <summary>
        /// 创建 审核
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> CreateAudit(CreateAuditRequset requset)
        {
             return ApiActuator.Runing(requset, (requset1) =>_AuditHandler.CreateAudit(requset));
        }



 

        /// <summary>
        /// 审核 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetAuditResponse> GetAudit(GetAuditRequest requset)
        {
             return ApiActuator.Runing(requset, (requset1) =>_AuditHandler.GetAuditList(requset));
        }


        /// <summary>
        /// 审核 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> Audit(AuditInfo requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _AuditHandler.Audit(requset));
        }


        
    }
}