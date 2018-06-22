using Eagles.Application.Model.Operator.Model;

namespace Eagles.Application.Model.Operator.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetOperatorDetailResponse : ResponseBase
    {
        /// <summary>
        /// 
        /// </summary>
        public OperatorDetail Info { get; set; }
    }
}
