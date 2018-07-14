using Eagles.Base;
using Eagles.Base.Configuration;
using Eagles.Base.DataBase;
using Eagles.Base.DataBase.Implement;
using Eagles.Base.Json;
using Eagles.Base.Json.Implement;
using Eagles.Base.Logger;
using Eagles.Base.Logger.Implement;
using Eagles.Interface.DataAccess;
using Ealges.DomianService.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Eagles.Application.Host.Common
{
    /// <summary>
    /// 逻辑调用执行器
    /// </summary>
    public class ApiActuator
    {
        private static readonly ILogger Logger = new Logger();
        private static readonly IJsonSerialize json = new JsonSerialize();
        private static readonly IConfigurationManager configuration = new Eagles.Base.Configuration.Implement.ConfigurationManager();
        private static readonly IDbManager dbManager = new DbManager(configuration, Logger);
        private static readonly ILoginDataAccess dataAccess = new LoginDataAccess(dbManager);
        /// <summary>
        /// 逻辑调用执行器
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="data"></param>
        /// <param name="run">执行</param>
        /// <returns>返回格式化数据</returns>
        public static ResponseFormat<TResult> Runing<TResult, T>(T data, Func<T, TResult> run,bool needCheckToken=true)
        {
            try
            {
                if (needCheckToken)
                {
                    var Requset = json.SerializeObject(data);
                    var RequsetData = json.Deserialize<Dictionary<string, object>>(Requset);
                    var token = RequsetData["Token"].ToString();
                    var info = dataAccess.GetUserToken(token, 1);
                    if (info == null || info.UserId <= 0)
                        throw new TransactionException("500", "token校验失败");
                }
              
                // var aa = (object)run.Target;
                return new ResponseFormat<TResult>(run.Invoke(data));
            }
            catch (TransactionException err)
            {
                Logger.LoggerInfo(err.ToString());
                return new ResponseFormat<TResult>(default(TResult), err.ErrorCode, err.ErrorMessage);
            }
            catch (Exception err)
            {
                Logger.LoggerError(err.ToString());
                return new ResponseFormat<TResult>(default(TResult), "500", err.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="run"></param>
        /// <returns></returns>

        public static ResponseFormat<T> Runing<T>( Func<T> run)
        {
            try
            {
                return new ResponseFormat<T>(run.Invoke());
            }
            catch (TransactionException err)
            {
                Logger.LoggerInfo(err.ToString());
                return new ResponseFormat<T>(default(T), err.ErrorCode, err.ErrorMessage);
            }
            catch (Exception err)
            {
                Logger.LoggerError(err.ToString());
                return new ResponseFormat<T>(default(T), "500", err.Message);
            }
        }
    }
}