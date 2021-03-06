﻿namespace Eagles.Application.Model.Operator.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class OperatorDetail : Model.Operator
    {
        /// <summary>
        /// 密码
        /// </summary>
        /// <returns></returns>
        public string Password { get; set; }

        ///// <summary>
        ///// 账号
        ///// </summary>
        ///// <returns></returns>
        //public string Account { get; set; }

        /// <summary>
        /// 权限组id
        /// </summary>
        public int AuthorityGroupId { get; set; }

        /// <summary>
        /// 状态;0:正常;1:禁用
        /// </summary>
        public int Status { get; set; }
    }
}
