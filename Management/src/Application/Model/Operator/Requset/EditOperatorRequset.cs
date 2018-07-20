using Eagles.Application.Model.Operator.Model;

namespace Eagles.Application.Model.Operator.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class EditOperatorRequset: RequestBase
    {
        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public OperatorDetail Info { get; set; }
    }
}
