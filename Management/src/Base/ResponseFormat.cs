using System;

namespace Eagles.Base
{

    /// <summary>
    /// 返回数据格式
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class ResponseFormat<T>
    {
        /// <summary>
        /// 格式化反馈信息
        /// </summary>
        /// <param name="obj">数据对象</param>
        /// <param name="code">状态</param>
        /// <param name="message">消息提示</param>
        public ResponseFormat(T obj, string code = "00", string message = "执行成功")
        {
            this.Result = obj;
            this.Message = message;
            this.Code = code;
        }


        /// <summary>
        /// 返回数据信息
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime DateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 返回码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Message { get; set; }


    }

}