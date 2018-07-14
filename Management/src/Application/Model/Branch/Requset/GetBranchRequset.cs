namespace Eagles.Application.Model.Branch.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetBranchRequset : RequestBase
    {
        /// <summary>
        /// 凭证
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 凭证
        /// </summary>
        public int BranchId { get; set; }
    }
}
