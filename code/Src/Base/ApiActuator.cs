using System;
using Newtonsoft.Json;
using NLog;

namespace Eagles.Base
{
    /// <summary>
    /// 逻辑调用执行器
    /// </summary>
    public class ApiActuator
    {
        private static readonly Logger.ILogger Logger=new Logger.Implement.Logger();

        /// <summary>
        /// 逻辑调用执行器
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="run">执行</param>
        /// <returns>返回格式化数据</returns>
        public static ResponseFormat<T> Runing<T>(Func<T> run)
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