using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Audit.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateAuditRequset:RequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        public Model.Audit Info { get; set; }
    }
}
